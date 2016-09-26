using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AITest : MonoBehaviour {
	public Transform followTarget;
	public CharacterController con;
	public Animation ani;
	
	public float chouHen = 5f;
	public float attackDis = 2f;
	
	public float thinkTime = 1f;
	public float currentTime = 0f;
	
	public float walkTime = 5f;
	public float walkCurrentTime = 0f;
	
	
	public float walkThinkTime = 4f;
	public float walkThinkCurrentTime = 0f;
	
	int skillIndex = 1;
	
	bool isAttack = false;

	public float Hp = 0f;
	public float MaxHp = 100f;
	public Slider slider;

	// Use this for initialization
	void Start () {
		InvokeRepeating("RangeSkillIndex",3,3);//重复调用,从第一次调用开始,每隔repeatRate时间调用一次.
		Hp = 100f;
	}
	void RangeSkillIndex()
	{
		skillIndex =Random.Range(1,4);
	}
	
	// Update is called once per frame
	void Update () {
		//思考 一段时间
		Think();

		//让Boss看向目标
		LookTarget();

		updateSliderValue ();
		
	}
	
	void  LookTarget()
	{
		
		if(Vector3.Distance(transform.position,followTarget.position) < chouHen  ){
			transform.LookAt(followTarget);
			if(Vector3.Distance(transform.position,followTarget.position) > attackDis  )
			{
				Vector3 moveDirection = transform.TransformDirection(Vector3.forward);
				moveDirection.y -= 9.8f;
				con.Move(moveDirection*1f*Time.deltaTime);
				isAttack = false;
				ani.CrossFade("Run");
			}
			//抵达攻击区域，开始攻击
			else
			{
				ani.CrossFade("Attack");
				isAttack = true;
			}
		}
	}
	
	
	void Think()
	{
		if(isAttack)
			return;

		currentTime += Time.deltaTime;
		if(currentTime >= (thinkTime+walkTime+walkThinkTime))
		{
			currentTime = 0f;
		}
		else
		{
			walkCurrentTime+= Time.deltaTime;
			if(walkCurrentTime >= walkTime){
				ani.CrossFade("Idle");
				walkThinkCurrentTime+=Time.deltaTime;
				if(walkThinkCurrentTime>= walkThinkTime)
				{
					walkThinkCurrentTime = 0f;
					walkCurrentTime =0f;
					float x = Random.Range(-1f,1f);
					float z= Random.Range(-1f,1f);
					transform.LookAt(transform.position + new Vector3(x,0,z));
				}
			}
			else
			{
				//漫游
				ani.CrossFade("Run");
				Vector3 moveDirection = transform.TransformDirection(Vector3.forward);
				moveDirection.y -= 9.8f;
				con.Move(moveDirection*1f*Time.deltaTime);
			}
			
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		Debug.Log (other.name);
		if (Hp > 0) {
			if(other.tag.Equals("huangziwuqi") && HeroCtr.isAttack){
				Hp -= 100f;
			}

			if (other.tag.Equals ("jn") && HeroCtr.isAttack) {
				Hp -= 20f;
			}
		}

		if (Hp <= 0) {
			Destroy(this.gameObject);
		}
	}

	void updateSliderValue(){
		slider.value = Hp / MaxHp;
	}
	
}
