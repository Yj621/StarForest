using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    CowState theCowState;
    DropItem theDropItem;
    State theState;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator animator;
    public Vector2 inputVec;
    public float speed;

    public int cowHappiness = 0;
    public int chickenHappiness = 0;

    public bool isCow = false;
    public bool isChick = false;

    public bool isPlant = false;
    public bool isDig = false;


    //애니메이션
    private bool fish = false;
    public bool casting = false;
    public bool Planting = false;
    public bool Doing = false;

    public bool Done = false;
    private bool doNotWalk = false;
    private bool riverArea = false;
    private GameObject spaceBar;
    public GameObject soil_00;
    private RaycastHit2D hit; // hit 변수를 클래스 레벨로 이동
    public Transform targetObject; // 따라갈 오브젝트

    public bool haveRice = true;

    //시간
    private float timer = 0f;
    private float decreaseInterval = 5f; // 20초마다 감소

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
    }
    void Start()
    {
        spaceBar.SetActive(false);
    }

    //이동
    void Update()
    {

        //CheckItem();
        //TryAction();


        Ray();

        inputVec.x = Input.GetAxis("Horizontal");
        inputVec.y = Input.GetAxis("Vertical");

        // 스페이스바 입력 처리
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fish();

            if (Planting == true)  
            {
                doNotWalk = true;
                animator.SetBool("Dig", true);
                Invoke("Dig", 1f);  //1초 대기 후 Dig함수 실행

                if (isPlant == true)
                {

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
                // Debug.Log(cowHappiness);
            }
            if (isChick == true)
            {
                // Debug.Log("Chicken");
                chickenHappiness += 1;
                // Debug.Log(chickenHappiness);
            }
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
        // 레이를 매 프레임 그립니다.
        Debug.DrawRay(rayStart, rayDirection * rayLength, Color.red);

        // 레이를 발사하여 River 태그에 닿으면 casting을 true로 설정
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
            Done = true;
            theState.Progressbar();
            theDropItem.FishDrop();
            // if(theDropItem.isDrop == true)
            // {

            // }
        }
    }

    //낚시 끝날때
    void FishDone()
    {
        animator.SetBool("Fish", false);
        animator.SetBool("Casting", false); // casting 애니메이션 종료
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

    void Dig()
    {
        string objectTag = gameObject.tag;

        if (objectTag == "Player")
        {
            Instantiate(soil_00, Digged_soil_pos(this.transform.position), Quaternion.identity);
        }
        animator.SetBool("Dig", false);
        doNotWalk = false;
    }

    Vector2 Digged_soil_pos(Vector2 position)   //그리드에 맞춰 흙 배치
    {
        float x = position.x;
        float y = position.y;
        bool left = spriter.flipX; //바라보는 방향

        Debug.Log(x + ", " + y + left);
        //x좌표
        if (x % 1 >= 0.5f || x % 1 < -0.5f)
        {
            if (left)
            {
                x += 0.5f - (x % 1);
            }
            if (!left)
            {
                x += 1.5f - (x % 1);
            }

            if (position.x < 0)
            {
                x -= 2f;
            }
        }

        if(-0.5f <= x%1 && x%1 <0.5f)
        {
            if (left)
            {
                x -= 0.5f + (x % 1);
            }
            else
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

        Debug.Log(x + ", " + y + left);
        return new Vector2(x, y);
    }

    //Hair&Tool애니메이션
    void LateUpdate()
    {
        animator.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0 && !casting) // 방향키를 누르고 casting이 아닐 때만 플립합니다.
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
            Planting = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        string objectTag = gameObject.tag;
        //목축
        if (other.gameObject.CompareTag("Cow") && objectTag == "Player")
        {
            isCow = true;
            // Debug.Log("cow");
            // cowHappiness += 1;
            // Debug.Log(cowHappiness);
        }
        if (other.gameObject.CompareTag("Chicken") && objectTag == "Player")
        {
            isChick = true;
        }


        //if (objectTag == "Player" && other.gameObject.CompareTag("Chicken"))
        //{
        //    Debug.Log("Player");
        //}
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("RiverPoint"))
        {
            riverArea = false;
            casting = false;
            fish = false;
            animator.SetBool("Casting", false); // casting 애니메이션 종료
            animator.SetBool("Fish", false); // fish 애니메이션 종료
            spaceBar.SetActive(false);
        }

        if (other.gameObject.CompareTag("PlantPoint"))
        {
            theState.SpaceBarDone();
            Planting = false;
        }

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
        chickenHappiness -= 1;
        // Debug.Log("닭의 행복 지수 : " + chickenHappiness);
        // chickenHappiness 음수가 되지 않도록 예외 처리
        if (chickenHappiness <= 0)
        {
            chickenHappiness = 0;
        }
    }


    ///*
    //* 아이템 인벤토리
    //*/
    //private void TryAction()
    //{
    //    if (Input.GetKeyDown(KeyCode.Z))
    //    {
    //        CheckItem();
    //        CanPickUp();
    //    }
    //}

    //private void CheckItem()
    //{
    //    if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
    //    {
    //        if (hitInfo.transform.tag == "Item")
    //        {
    //            ItemInfoAppear();
    //        }
    //    }
    //    else
    //        ItemInfoDisappear();
    //}

    //private void ItemInfoAppear()
    //{
    //    pickupActivated = true;
    //}

    //private void ItemInfoDisappear()
    //{
    //    pickupActivated = false;
    //}

    //private void CanPickUp()
    //{
    //    if (pickupActivated)
    //    {
    //        if (hitInfo.transform != null)
    //        {
    //            Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " 획득 했습니다.");  // 인벤토리 넣기
    //            Destroy(hitInfo.transform.gameObject);
    //            ItemInfoDisappear();
    //        }
    //    }
    //}
}
