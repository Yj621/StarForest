using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    UiController theUI;
    CowState theCowState;
    DropItem theDropItem;
    State theState;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator animator;
    public Vector2 inputVec;
    public float speed;

    public int cowHappiness = 0;
    public int chickenHappiness;

    public bool isCow = false;
    public bool isChick = false;

    //농사에서 땅(오브젝트) 확인
    public bool isFarm = false;
    public bool isPlant = false;
    public bool isDig = false;


    //애니메이션(동작 확인)
    private bool fish = false;
    public bool casting = false;
    public bool Digging = false;
    public bool Doing = false;

    public int SetCropNo = 0;
    public bool Done = false;
    private bool doNotWalk = false;
    private bool riverArea = false;
    private GameObject spaceBar;
    public GameObject soil_00;
    public GameObject soil_01;
    private RaycastHit2D hit; // hit 변수를 클래스 레벨로 이동
    public Transform targetObject;
    public bool haveRice = true;

    //시간
    private float timer = 0f;
    private float decreaseInterval = 20f; // 20초마다 감소

    /*
     * 아이템 인벤토리
     */

    //[SerializeField]
    //private float range;
    //private bool pickupActivated = false;

    //private RaycastHit hitInfo;

    //[SerializeField]
    //private LayerMask layerMask;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        spaceBar = GameObject.Find("SpaceBar");

        theState = FindObjectOfType<State>();
        theDropItem = FindObjectOfType<DropItem>();
        theCowState = FindObjectOfType<CowState>();
        theUI = FindObjectOfType<UiController>();
    }
    void Start()
    {
        spaceBar.SetActive(false);
        chickenHappiness = 0;
    }

    //이동
    void Update()
    {

        //CheckItem();
        //TryAction();


        Ray();

        inputVec.x = Input.GetAxis("Horizontal");
        inputVec.y = Input.GetAxis("Vertical");

        //숫자키로 입력으로 심을 작물 종류 결정
        if(Input.GetKeyDown(KeyCode.Alpha0))    //비트
        {
            SetCropNo = 0;
        }
       else if(Input.GetKeyDown(KeyCode.Alpha1))    //당근
        {
            SetCropNo = 1;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))   //파스닙
        {
            SetCropNo = 2;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))   //호박
        {
            SetCropNo = 3;
        }

        // 스페이스바 입력 처리
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fish();

            //농사
            if (isFarm)
            {
                if(!Digging && !Doing)  //동작이 끝났는지 확인
                {
                    GameObject targetObj = FindSoil();  //작업할 위치의 땅 확인

                    //기본 흙일때 -> 땅파기
                    if (!isDig && !isPlant)
                    {
                        StartCoroutine(Dig());
                    }
                    else if (isDig)
                    {
                        //파진 흙일때 -> 씨앗심기
                        if (!isPlant)
                        {
                            StartCoroutine(Seed(targetObj));
                        }
                        //식물이 심어진 흙일때
                        else if (isPlant)
                        {
                            //지정한 땅에 식물이 다 자랐는지 확인하는 변수 가져옴
                            Transform Crops = targetObj.transform.Find("Crops");
                            bool GrowEnd = Crops.GetComponent<CropsGrow>().GrowEnd;
                            int CropNo = Crops.GetComponent<CropsGrow>().CropNo;

                            //식물이 다 자랐으면
                            if (GrowEnd)
                            {
                                StartCoroutine(Harvest(targetObj, CropNo));
                            }
                            isPlant = false;
                        }
                        isDig = false;
                    }
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {

            if (Done == true)
            {
                FishDone();
            }

            if (isCow == true)
            {
                // Debug.Log("cow");
                cowHappiness += 1;
                Debug.Log("소: " + cowHappiness);
            }
            if (isChick == true)
            {
                Debug.Log(chickenHappiness);
                // Debug.Log("Chicken");
                chickenHappiness += 1;

            }
            Debug.Log("chickenHappiness: " + chickenHappiness);

        }
        // 머리, Tool따로 노는거 방지
        if (targetObject != null)
        {
            transform.position = targetObject.position;
        }

        // 타이머를 업데이트
        timer += Time.deltaTime;

        // 주기가 지났을 때 함수 호출 및 타이머 리셋
        if (timer >= decreaseInterval)
        {
            DecreaseCowHappiness();
            DecreaseChickHappiness();
            timer = 0f;
        }
    }

    //레이 함수
    void Ray()
    {
        // 레이 시작점
        Vector2 rayStart = transform.position;
        // 레이 방향
        // 플레이어의 스프라이트 방향을 고려하여 레이 방향 설정
        Vector2 rayDirection = spriter.flipX ? Vector2.left : Vector2.right;

        // 레이 길이
        float rayLength = 3.0f;
        // 레이를 매 프레임 그린다
        Debug.DrawRay(rayStart, rayDirection * rayLength, Color.red);

        // 레이를 발사하여 River 태그에 닿으면 riverArea을 true로 설정
        RaycastHit2D hit = Physics2D.Raycast(rayStart, rayDirection, rayLength, LayerMask.GetMask("River"));

        if (hit.collider != null)
        {
            riverArea = true;
        }
        else
        {
            riverArea = false;
        }
    }
    //낚시 함수
    void Fish()
    {
        string objectTag = gameObject.tag;
        if (fish == false && casting == true && riverArea == true) //fish 애니메이션이 실행 중이지 않을 때
        {
            // casting = true; // casting 애니메이션 시작
            animator.SetBool("Casting", true);
            fish = true;
            doNotWalk = true;
        }
        else if (casting && fish && Done == false)
        {
            // fish 애니메이션 시작
            casting = false;
            animator.SetBool("Casting", false); // casting 애니메이션을 끝냄
            animator.SetBool("Fish", true);

            if (objectTag == "Player")
            {
                theState.Progressbar();
                theDropItem.FishDrop();
            }
            Done = true;
        }
    }

    //낚시 끝날때
    void FishDone()
    {
        animator.SetBool("Fish", false);
        Done = false;
        casting = false;
        fish = false;
        doNotWalk = false;
        theState.ProgressbarDone();
    }


    void FixedUpdate()
    {
        if (!doNotWalk)
        {
            Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
            //위치
            rigid.MovePosition(rigid.position + nextVec);
        }
    }

    GameObject FindSoil()
    {
        string objectTag = gameObject.tag;
        Vector2 targetPosition = Grid_pos();
        GameObject[] Digged_soil = GameObject.FindGameObjectsWithTag("Digged_soil");
        GameObject[] Planted_soil = GameObject.FindGameObjectsWithTag("Planted_soil");

        foreach (GameObject obj in Digged_soil)  //배열의 반복문
        {
            //현재 플레이어 좌표에 맞는 그리드 좌표에 위치한 'soil_00' 오브젝트 찾기
            if (obj.transform.position.x == targetPosition.x && obj.transform.position.y == targetPosition.y)
            {
                isDig = true;
                isPlant = false;
                return obj;
            }
        }
        foreach (GameObject obj in Planted_soil)  //배열의 반복문
        {
            //현재 플레이어 좌표에 맞는 그리드 좌표에 위치한 'soil_01' 오브젝트 찾기
            if (obj.transform.position.x == targetPosition.x && obj.transform.position.y == targetPosition.y)
            {
                isDig = true;
                isPlant = true;
                return obj;
            }
        }
        isDig = false;
        isPlant = false;
        return null;
    }

    //땅파기 함수
    IEnumerator Dig()
    {
        string objectTag = gameObject.tag;

        doNotWalk = true;
        Digging = true;
        animator.SetBool("Dig", true);

        yield return new WaitForSeconds(1f);    //1초 대기

        if (objectTag == "Player")
        {
            Instantiate(soil_00, Grid_pos(), Quaternion.identity);
        }
        animator.SetBool("Dig", false);
        doNotWalk = false;
        Digging = false;
    }

    IEnumerator Seed(GameObject obj)
    {
        string objectTag = gameObject.tag;

        doNotWalk = true;
        Doing = true;
        animator.SetBool("Doing", true);
        yield return new WaitForSeconds(0.7f);    //0.7초 대기

        if (objectTag == "Player")
        {
            Instantiate(soil_01, Grid_pos(), Quaternion.identity);
            Destroy(obj);   //기존에 있던 soil_00 제거
        }
        animator.SetBool("Doing", false);
        doNotWalk = false;
        Doing = false;
    }

    IEnumerator Harvest(GameObject obj, int num)
    {
        string objectTag = gameObject.tag;

        doNotWalk = true;
        Doing = true;
        animator.SetBool("Doing", true);
        yield return new WaitForSeconds(0.7f);    //0.7초 대기

        if (objectTag == "Player")
        {
            Destroy(obj);   //기존에 있던 soil_01 제거
            //아이템 생성
            Instantiate(theDropItem.CropDrop(num), Grid_pos(), Quaternion.identity);

        }
        animator.SetBool("Doing", false);
        doNotWalk = false;
        Doing = false;
    }

    //그리드에 맞추기
    Vector2 Grid_pos()
    {
        float x = targetObject.transform.position.x;
        float y = targetObject.transform.position.y;
        bool left = spriter.flipX; //바라보는 방향

        //Debug.Log(x + ", " + y + left);

        //x좌표
        if (x % 1 >= 0.5f || x % 1 < -0.5f)
        {
            if (left)   //왼쪽 바라봄
            {
                x += 0.5f - (x % 1);
            }
            else  //오른쪽 바라봄
            {
                x += 1.5f - (x % 1);
            }

            if (targetObject.transform.position.x < 0)
            {
                x -= 2f;
            }
        }

        if (-0.5f <= x % 1 && x % 1 < 0.5f)
        {
            if (left)   //왼쪽 바라봄
            {
                x -= 0.5f + (x % 1);
            }
            else        //오른쪽 바라봄
            {
                x += 0.5f - (x % 1);
            }
        }

        //y좌표
        if (y % 1 > 0)
        {
            y += 0.5f - (y % 1);
        }
        if (y % 1 == 0)
        {
            y += 0.5f;
        }
        if (y % 1 < 0)
        {
            y -= 0.5f + (y % 1);
        }

        //Debug.Log(x + ", " + y + left);
        return new Vector2(x, y);
    }



    //Hair&Tool애니메이션
    void LateUpdate()
    {
        animator.SetFloat("Speed", inputVec.magnitude);

        // 방향키를 누르고 casting, Digging, Doing이 아닐 때만 플립
        if (inputVec.x != 0 && !casting && !Digging && !Doing)
        {
            spriter.flipX = inputVec.x < 0;

            // 자식 객체도 플립되지 않도록 처리
            foreach (Transform child in transform)
            {
                SpriteRenderer childSpriter = child.GetComponent<SpriteRenderer>();

                if (childSpriter != null)
                {
                    childSpriter.flipX = inputVec.x < 0;
                }
            }
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        //낚시
        if (other.gameObject.CompareTag("RiverPoint") && riverArea == true)
        {
            spaceBar.SetActive(true); //스페이스바 아이콘
            casting = true;
        }

        //농사
        if (other.gameObject.CompareTag("PlantPoint")) //밭
        {
            theState.SpaceBar();//스페이스바 띄우기
            isFarm = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        string objectTag = gameObject.tag;
        //목축
        if (other.gameObject.CompareTag("Cow") && objectTag == "Player_Tool")
        {
            isCow = true;
        }
        if (other.gameObject.CompareTag("Chicken") && objectTag == "Player_Tool")
        {
            isChick = true;
            Debug.Log(chickenHappiness);
        }

        if (other.gameObject.CompareTag("Npc") && objectTag == "Player_Hair")
        {
            theUI.StorePanel.SetActive(true);
        }

        if (other.gameObject.CompareTag("Store") )
        {
            transform.position = new Vector3(-67.1f, 11.36f, transform.position.z);
            Debug.Log("Enter");
        }
        if (other.gameObject.CompareTag("Play"))
        {
            transform.position = new Vector3(0.5f, 4.3f, transform.position.z);
            Debug.Log("Play");
        }

        //if (objectTag == "Player" && other.gameObject.CompareTag("Chicken"))
        //{
        //    Debug.Log("Player");
        //}
    }


    void OnTriggerExit2D(Collider2D other)
    {
        //낚시
        if (other.gameObject.CompareTag("RiverPoint"))
        {
            riverArea = false;
            casting = false;
            fish = false;
            animator.SetBool("Casting", false); // casting 애니메이션 종료
            animator.SetBool("Fish", false); // fish 애니메이션 종료
            spaceBar.SetActive(false);
        }

        //농사
        if (other.gameObject.CompareTag("PlantPoint"))
        {
            theState.SpaceBarDone();
            isFarm = false;
            isDig = false;
            isPlant = false;
        }

        //목축
        if (other.gameObject.CompareTag("Cow"))
        {
            isCow = false;
        }
        if (other.gameObject.CompareTag("Chicken"))
        {
            isChick = false;
        }
    }

    // 동물 행복도 감소 함수
    private void DecreaseCowHappiness()
    {
        cowHappiness -= 1;
        // Debug.Log("소의 행복 지수 : " + cowHappiness);
        // cowHappiness가 음수가 되지 않도록 예외 처리
        if (cowHappiness <= 0)
        {
            cowHappiness = 0;
        }
    }
    private void DecreaseChickHappiness()
    {

        // Debug.Log("닭의 행복 지수 : " + chickenHappiness);
        // chickenHappiness 음수가 되지 않도록 예외 처리
        if (chickenHappiness <= 0)
        {
            chickenHappiness = 0;
        }
        else
        {
            chickenHappiness -= 1;
        }
    }
}


//    /*
//    * 아이템 인벤토리
//    */
//    private void TryAction()
//    {
//        if (Input.GetKeyDown(KeyCode.Z))
//        {
//            CheckItem();
//            CanPickUp();
//            print("pick z");
//        }
//    }

//    private void CheckItem()
//    {
//        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
//        {
//            if (hitInfo.transform.tag == "Item")
//            {
//                ItemInfoAppear();
//            }
//        }
//        else
//            ItemInfoDisappear();
//    }

//    private void ItemInfoAppear()
//    {
//        pickupActivated = true;
//    }

//    private void ItemInfoDisappear()
//    {
//        pickupActivated = false;
//    }
//    private void CanPickUp()
//    {
//        if (pickupActivated)
//        {
//            if (hitInfo.transform != null)
//            {
//                try
//                {
//                    ItemPickUp itemPickUp = hitInfo.transform.GetComponent<ItemPickUp>();
//                    if (itemPickUp != null)
//                    {
//                        Debug.Log(itemPickUp.item.itemName + " 획득 했습니다.");  // 인벤토리 넣기
//                        Destroy(hitInfo.transform.gameObject);
//                        ItemInfoDisappear();
//                    }
//                    else
//                    {
//                        Debug.Log("ItemPickUp 컴포넌트를 찾을 수 없습니다.");
//                    }
//                }
//                catch (Exception e)
//                {
//                    Debug.LogError("예외 발생: " + e.Message);
//                }
//            }
//        }
//    }
//}
