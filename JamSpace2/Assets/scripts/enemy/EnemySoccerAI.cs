using UnityEngine;

public class EnemySoccerAI : MonoBehaviour
{
    [Header("References")]
    public Transform ball;
    public Transform player;
    public Transform targetGoal;

    [Header("Penalty Areas")]
    public Collider2D[] penaltyAreas;
    public float exitForce = 6f;

    [Header("Goal Exclusion")]
    public Collider2D ownGoalArea;   // Enemy's own goal
    public float goalExitForce = 6f;

    [Header("Movement")]
    public float moveSpeed = 3f;
    public float sprintSpeed = 6f;
    public float sprintCooldown = 8f;
    public float stopDistance = 0.6f;

    [Header("Positioning")]
    public float stealOffset = 1.2f;
    public float sideStepDistance = 1.5f;

    [Header("Ball Handling")]
    public float wallCheckDistance = 1f;
    public float ballStillSpeed = 0.3f;
    public float kickForce = 6f;

    private Rigidbody2D rb;
    private Rigidbody2D ballRb;
    private SoccerPush ballScript;

    private float lastSprintTime = -100f;
    private bool isSprinting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ballRb = ball.GetComponent<Rigidbody2D>();
        ballScript = ball.GetComponent<SoccerPush>();
    }

    void FixedUpdate()
    {
        HandlePenaltyAreas();
        HandleOwnGoalArea();

        if (ShouldWaitForBall())
            return;

        Vector2 targetPos = DecideInterceptPosition();
        targetPos = AvoidWalls(targetPos);

        HandleSprint(targetPos);

        // MovePosition for smooth movement
        Vector2 currentPos = rb.position;
        float speed = isSprinting ? sprintSpeed : moveSpeed;
        float step = speed * Time.fixedDeltaTime;

        if (Vector2.Distance(currentPos, targetPos) > stopDistance)
        {
            Vector2 newPos = Vector2.MoveTowards(currentPos, targetPos, step);
            rb.MovePosition(newPos);
        }
    }

    // Multi-penalty area handling
    void HandlePenaltyAreas()
    {
        if (penaltyAreas == null || penaltyAreas.Length == 0) return;

        foreach (Collider2D area in penaltyAreas)
        {
            if (area.OverlapPoint(rb.position))
            {
                Vector2 closest = area.ClosestPoint(rb.position);
                Vector2 dirOut = ((Vector2)rb.position - closest).normalized;
                rb.AddForce(dirOut * exitForce, ForceMode2D.Impulse);
            }
        }
    }

    // Own goal exclusion
    void HandleOwnGoalArea()
    {
        if (ownGoalArea && ownGoalArea.OverlapPoint(rb.position))
        {
            Vector2 closest = ownGoalArea.ClosestPoint(rb.position);
            Vector2 dirOut = ((Vector2)rb.position - closest).normalized;
            rb.AddForce(dirOut * goalExitForce, ForceMode2D.Impulse);
        }
    }

    // Wait if ball is bouncing near walls
    bool ShouldWaitForBall()
    {
        if (ballRb.linearVelocity.magnitude > ballStillSpeed)
            return false;

        RaycastHit2D hit = Physics2D.Raycast(
            ball.position,
            ballRb.linearVelocity.normalized,
            wallCheckDistance,
            LayerMask.GetMask("Wall")
        );

        return hit.collider != null;
    }

    // Tactical intercept position (side-step around player)
    Vector2 DecideInterceptPosition()
    {
        Vector2 ballPos = ball.position;
        Vector2 playerPos = player.position;

        Vector2 toBallFromPlayer = (ballPos - playerPos).normalized;
        Vector2 toBallFromEnemy = (ballPos - rb.position).normalized;
        float dot = Vector2.Dot(toBallFromPlayer, toBallFromEnemy);

        if (dot < 0.8f) // player in path
        {
            Vector2 sideOffset = Vector2.Perpendicular(toBallFromPlayer).normalized * sideStepDistance;
            if (Vector2.Dot(sideOffset, rb.position - playerPos) < 0)
                sideOffset = -sideOffset;

            return ballPos + toBallFromPlayer * stealOffset + sideOffset;
        }

        return ballPos;
    }

    // Raycast-based wall avoidance
    Vector2 AvoidWalls(Vector2 targetPos)
    {
        Vector2 dir = (targetPos - rb.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(rb.position, dir, wallCheckDistance, LayerMask.GetMask("Wall"));

        if (hit.collider != null)
        {
            Vector2 perp = Vector2.Perpendicular(dir).normalized;

            RaycastHit2D leftCheck = Physics2D.Raycast(rb.position, perp, wallCheckDistance, LayerMask.GetMask("Wall"));
            RaycastHit2D rightCheck = Physics2D.Raycast(rb.position, -perp, wallCheckDistance, LayerMask.GetMask("Wall"));

            if (leftCheck.collider == null)
                return rb.position + perp * moveSpeed * Time.fixedDeltaTime;
            else if (rightCheck.collider == null)
                return rb.position - perp * moveSpeed * Time.fixedDeltaTime;
            else
                return rb.position; // stuck, wait
        }

        return targetPos;
    }

    // Tactical sprint with cooldown
    void HandleSprint(Vector2 targetPos)
    {
        float distance = Vector2.Distance(rb.position, targetPos);
        bool pathClear = IsPathClear(targetPos);

        if (!isSprinting && Time.time - lastSprintTime > sprintCooldown && distance > 5f && pathClear)
        {
            isSprinting = true;
            lastSprintTime = Time.time;
        }

        if (isSprinting && Time.time - lastSprintTime > 1.5f)
            isSprinting = false;
    }

    bool IsPathClear(Vector2 targetPos)
    {
        Vector2 dir = (targetPos - rb.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(rb.position, dir, Vector2.Distance(rb.position, targetPos));
        if (hit.collider != null && hit.collider.transform == player)
            return false;
        return true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.CompareTag("Ball"))
            return;

        ballScript.isPlayerTouchingBall = false;

        Vector2 kickDir = ((Vector2)targetGoal.position - ballRb.position).normalized;
        ballRb.AddForce(kickDir * kickForce, ForceMode2D.Impulse);
    }
}
