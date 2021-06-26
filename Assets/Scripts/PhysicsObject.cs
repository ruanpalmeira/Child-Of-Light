using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CircleCollider2D))]
public class PhysicsObject : MonoBehaviour{

/*    CircleCollider2D _collider;
    const float skinwidth = 0.015f; 
	public int horizontalRayCount = 4;
	public int verticalRayCount = 4;
    public LayerMask collisionMask;

	float horizontalRaySpacing;
	float verticalRaySpacing;

	RaycastOrigins raycastOrigins;
    public CollisionsInfo collisions;

    private void Start() {
        _collider = GetComponent<CircleCollider2D>();
        calculateRaySpacing();
    }

    public void Move(Vector3 velocity){
        UpdateRaycastOrigins();
        collisions.Reset();

        if(velocity.x != 0){
            HorizontalCollisions(ref velocity);
        }
        if(velocity.y != 0){
            VerticalCollisions(ref velocity);
        }
        
        transform.Translate(velocity);
    }

    void HorizontalCollisions(ref Vector3 velocity){
        float directionX = Mathf.Sign(velocity.x);
        float rayLenght = Mathf.Abs(velocity.x) + skinwidth;

        for (int i = 0; i < horizontalRayCount; i++) {
            Vector2 rayOrigin = (directionX == -1)?raycastOrigins.bottomLeft:raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLenght, collisionMask);
			
            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLenght, Color.red);

            if(hit){
                velocity.x = (hit.distance - skinwidth) * directionX;
                rayLenght = hit.distance;

                collisions.left = directionX == -1;
                collisions.right = directionX == 1;
            }
        }
    }

    void VerticalCollisions(ref Vector3 velocity){
        float directionY = Mathf.Sign(velocity.y);
        float rayLenght = Mathf.Abs(velocity.y) + skinwidth;

        for (int i = 0; i < verticalRayCount; i++) {
            Vector2 rayOrigin = (directionY == -1)?raycastOrigins.bottomLeft:raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLenght, collisionMask);
			Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLenght, Color.red);
		
            if(hit){
                velocity.y = (hit.distance - skinwidth) * directionY;
                rayLenght = hit.distance;

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }
        }
    }

	void UpdateRaycastOrigins(){
		Bounds bounds = _collider.bounds;
		bounds.Expand(skinwidth * -2);

		raycastOrigins.bottomLeft = new Vector2(bounds.min.x,bounds.min.y);
		raycastOrigins.bottomRight = new Vector2(bounds.max.x,bounds.min.y);
		raycastOrigins.topLeft = new Vector2(bounds.min.x,bounds.max.y);
		raycastOrigins.topRight = new Vector2(bounds.max.x,bounds.max.y);

	}

	void calculateRaySpacing(){
		Bounds bounds = _collider.bounds;
		bounds.Expand(skinwidth * -2);

		horizontalRayCount = Mathf.Clamp(horizontalRayCount,2,int.MaxValue);
		verticalRayCount = Mathf.Clamp(verticalRayCount,2,int.MaxValue);

		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
		verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
	}

	struct RaycastOrigins {
		public Vector2 topLeft,topRight;
		public Vector2 bottomLeft,bottomRight;
	}

    public struct CollisionsInfo{
        public bool above, below;
        public bool left, right;

        public void Reset() {
            above = below = false;
            left = right = false;
        }
    }
*/

    
    public float _minGroundNormalY = 0.65f;
    public float _gravityModifier = 2.0f;

    protected bool _grounded;
    protected ContactFilter2D contactFilter;
    protected Rigidbody2D rb2d;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
    protected Vector2 _velocity;
    protected Vector2 _groundNormal;
    protected Vector2 _targetVelocity;
    
    protected const float shellRadius = 0.01f;
    const float minMoveDistance = 0.001f;

    void OnEnable() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start(){

        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    void Update() {
        _targetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity(){

    }

    // Update is called once per frame
    void FixedUpdate(){
        _velocity += _gravityModifier * Physics2D.gravity * Time.deltaTime;
        _velocity.x = _targetVelocity.x;

        _grounded = false;

        Vector2 _deltaPosition = _velocity * Time.deltaTime;
        Vector2 _moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        Vector2 _move = _moveAlongGround * _deltaPosition.x;
        Movement(_move, false);
        _move = Vector2.up * _deltaPosition.y;
        Movement(_move, true);
    }
        
    /*
        if(_controller.isGrounded == true){
            if(Input.GetKeyDown(KeyCode.Space)){
                _doubleJump = true;
                _yVelocity = _jumpHeight;
            }
        }else{
            if(Input.GetKeyDown(KeyCode.Space) && _doubleJump == true){
                _doubleJump = false;
                _yVelocity += _jumpHeight;
            }
            _yVelocity -= _gravity;
        }

        velocity.y = _yVelocity;
        _controller.Move(velocity * Time.deltaTime);*/ //////////////////

    void Movement(Vector2 move, bool yMovement){

        float _distance = move.magnitude;

        if(_distance > minMoveDistance){
            int count = rb2d.Cast(move, contactFilter, hitBuffer, _distance + shellRadius);
            hitBufferList.Clear();

            for(int i = 0; i < count; i++){
                hitBufferList.Add(hitBuffer[i]);
            }
            if (hitBufferList.Count == 0) {
                _groundNormal = Vector2.up;
            }
            for(int i = 0; i < hitBufferList.Count; i++){
                Vector2 _currentNormal = hitBufferList[i].normal;

                if(_currentNormal.y > _minGroundNormalY){
                    _grounded = true;

                    if(yMovement){
                        _groundNormal = _currentNormal;
                        _currentNormal.x = 0;
                    }
                }
                float projection = Vector2.Dot(_velocity, _currentNormal);

                if(projection< 0){
                    _velocity = _velocity - projection * _currentNormal;
                }
                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                _distance = modifiedDistance< _distance ? modifiedDistance : _distance;
            }
        }
        rb2d.position = rb2d.position + move.normalized * _distance;
    }
}