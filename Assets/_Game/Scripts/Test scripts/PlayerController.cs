using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using HoldableNameSpace;
using DG.Tweening;

namespace PlayerNamespace
{
    public enum PlayerStates
    {
        Default, Holding
    }
    public class PlayerController : MonoBehaviour
    {

        public CharacterController characterController = null;
        public Joystick joystick = null;

        public GameObject playerModel = null;

        public float movementSpeed = 1f;

        public Canvas joystickCanvas = null;

        public CinemachineVirtualCamera followingCamera = null;

        //public List<PlantInfo> backpackPlantsList = null;

        public GuidingIndicator guidingIndicator = null;

        public GameObject placeForIngredient = null;

        private Vector3 moveVector = Vector3.zero;

        //  private Vector3 prevMoveVector = Vector3.zero;
        private float prevJoystickMagnitude = 0f;

        private Animator playerAnimator = null;

        // private GameController gameController = null;

        private bool hasIngredient = false;

        private HoldableObject heldObject = null;

        private PlayerStates playerState;

        // private HashSet<PlantInfo.PlantType> addedPlantsSet = new HashSet<PlantInfo.PlantType>();

        //  private Dictionary<PlantInfo.PlantType, GameObject> addedPlantsUniqueModels = new Dictionary<PlantInfo.PlantType, GameObject>();

        public bool HasBucketOfWater { get => hasIngredient; set => hasIngredient = value; }
        public Animator PlayerAnimator { get => playerAnimator; set => playerAnimator = value; }
        public PlayerStates PlayerState { get => playerState; set => playerState = value; }
        public HoldableObject HeldObject { get => heldObject; set => heldObject = value; }

        private void Awake()
        {
            playerAnimator = playerModel.GetComponent<Animator>();
        }
        // Start is called before the first frame update
        void Start()
        {
          //  GameObject testTarget = GameObject.Find("Ingredient box burger bun (1)");
          //  guidingIndicator.SetTargetAndEnable(testTarget.transform);
            // playerAnimator.SetTrigger("Walk");
            //playerAnimator.SetFloat("speed", 0f);
            //  joystick.OnPointerDown(new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.));
            //joystick.OnPointerUp(null);
        }

        // Update is called once per frame
        void Update()
        {
            //prevMoveVector = moveVector;

            moveVector.x = joystick.Horizontal;
            moveVector.z = joystick.Vertical;
            moveVector *= movementSpeed * Time.deltaTime;
            characterController.Move(moveVector);

            if (moveVector != Vector3.zero) playerModel.transform.forward = moveVector.normalized;

            // if(moveVector.magnitude>0){
            playerAnimator.SetFloat("speed", joystick.Direction.magnitude);

            /*         if (playerAnimator.GetBool("isWalking") != (joystick.Direction.magnitude > 0))
                    {
                        playerAnimator.SetBool("isWalking", joystick.Direction.magnitude > 0);
                    } */

            if ((playerAnimator.GetBool("isWalking") != (joystick.Direction.magnitude > 0)) && !hasIngredient)
            {
                if (playerAnimator.GetBool("isBucketWalking")) playerAnimator.SetBool("isBucketWalking", false);
                playerAnimator.SetBool("isWalking", joystick.Direction.magnitude > 0);
            }

            if ((playerAnimator.GetBool("isBucketWalking") != (joystick.Direction.magnitude > 0)) && hasIngredient)
            {
                playerAnimator.SetBool("isBucketWalking", true);
                if (playerAnimator.GetBool("isWalking")) playerAnimator.SetBool("isWalking", false);
            }


            //Debug.Log(joystick.Direction.magnitude);
            //}
            prevJoystickMagnitude = joystick.Direction.magnitude;
        }

        public void SetHoldableObject(HoldableObject holdableObject, bool shouldDestroyHoldable = false)
        {
            if (holdableObject == null)//just remove the holdable object and return player to default
            {
                if (shouldDestroyHoldable) Destroy(heldObject.gameObject);
                TriggerIdleAnim();
                heldObject = null;
                playerState = PlayerStates.Default;
                return;
            }

            holdableObject.transform.position = placeForIngredient.transform.position;
            //            holdableObject.transform.SetParent(placeForIngredient.transform);
            holdableObject.transform.parent = placeForIngredient.transform;
            //holdableObject.transform.rotation=Quaternion.Euler(0,0,0);
            //holdableObject.transform.rotation=Quaternion.identity;

            TriggerHoldingAnim();

            heldObject = holdableObject;

            playerState = PlayerStates.Holding;
        }

        public void TriggerHoldingAnim()
        {
            playerAnimator.SetBool("isBucketWalking", true);
            if (playerAnimator.GetBool("isWalking")) playerAnimator.SetBool("isWalking", false);
            hasIngredient = true;
        }

        public void TriggerIdleAnim()
        {
            playerAnimator.SetBool("isWalking", true);
            playerAnimator.SetTrigger("Idle");
            if (playerAnimator.GetBool("isBucketWalking")) playerAnimator.SetBool("isBucketWalking", false);
            hasIngredient = false;
        }

        public void SetBucketExistence(bool isEnabled)
        {
            hasIngredient = isEnabled;
            //bucketModel.SetActive(isEnabled);
        }

        public void SetJoystickEnabledAndVisible(bool isEnabled)
        {
            //joystickCanvas.gameObject.SetActive(isEnabled);
            if (!isEnabled) joystick.OnPointerUp(null);
            joystickCanvas.enabled = isEnabled;
            joystick.enabled = isEnabled;

            // joystick.
            //joystick.Vertical
        }
/*         private void OnTriggerEnter(Collider other) {
            guidingIndicator.PlayerTriggered(other.transform);
        } */

        public void SuccesfulTrigger(Transform otherTransform){
            guidingIndicator.PlayerTriggered(otherTransform);
        }

        /*     public void AddPlantToBackpack(PlantInfo plant, GameObject plantModel, float marketScaleBy = 1f)
            {
                if (backpackPlantsList == null)
                {
                    backpackPlantsList = new List<PlantInfo>();
                    SetBackpackFull(true);
                }

                backpackPlantsList.Add(plant);



                if (!addedPlantsSet.Contains(plant.Type))
                {
                    addedPlantsSet.Add(plant.Type);

                    GameObject plantModelCopy = Instantiate(plantModel);  //the plant model will be destroyed so we make a copy here
                    plantModelCopy.transform.localScale *= plantModel.transform.parent.localScale.x;//sets the scale correctly for world coords
                    plantModelCopy.transform.localScale *= marketScaleBy;
                    plantModelCopy.SetActive(false);

                    addedPlantsUniqueModels.Add(plant.Type, plantModelCopy); //adding one model for each plant type (to do the selling in the market effect)

                }
            } */

        /*     public void SellBackpackContents(Transform marketTransform)
            {
                if (backpackPlantsList == null) return;
                if (backpackPlantsList.Count == 0) return;

                SellBackpackContentsVisualEffect(marketTransform);
                int combinedPrice = 0;
                foreach (PlantInfo p in backpackPlantsList)
                {
                    combinedPrice += p.Price;
                    //check for order completion            
                }
                if (gameController == null) gameController = GameController.Instance;
                gameController.AddMoneyIncrementally(combinedPrice);

                backpackPlantsList = null;


                guidingIndicator.SetTargetAndEnable(gameController.GetClosestPlantTriggerCircle(transform));    //set the target to the closest plant circle when backpack empty
            } */



        /*     public void SetBackpackFull(bool isFull)
            {
                backpackModel.SetActive(!isFull);
                backpackFullModel.SetActive(isFull);
            } */
    }
}