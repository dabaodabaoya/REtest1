using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeroCtr : MonoBehaviour {
	public GameObject cam;
	
	private CharacterController m_ctr;  //角色控制器
	private Vector3 m_direction;  //移动方向
	private float m_gravity=10.0f;
	
	private Animator ani;
	public EasyJoystick myjoystick;
	private bool isRun;
	private enum State{
		Idle,
		Attack1,
		Attack2,
		Attack3,
	}
	private State _state=State.Idle;
	private int _hitCount=0;
	private AnimatorStateInfo _info;
	
	
	public static bool isAttack;
	
	public GameObject jn1;
	public GameObject jn2;
	public GameObject jn3;

	public float Hp = 0f;
	public float maxHp = 100f;
	public Slider slider;
	
	// Use this for initialization
	void Start () {
		ani = GetComponent<Animator> ();
		_info=ani.GetCurrentAnimatorStateInfo(0);
		m_ctr = GetComponent<CharacterController> ();

		Hp = maxHp;
	}
	
	// Update is called once per frame
	void Update () {
		updatePercent ();
		
		_info=ani.GetCurrentAnimatorStateInfo(0);
		StateJudge();
//		
//		if (isAttack)
//		{
//			GameObject.Find("tuowei").GetComponent<TrailRenderer>().enabled = true;
//		}
//		else
//		{
//			GameObject.Find("tuowei").GetComponent<TrailRenderer>().enabled = false;
//		}
//		
		//cam.transform.position = Vector3.Lerp (cam.transform.position, transform.position + new Vector3 (0, 2, -4), 0.1f);
		cam.transform.position = transform.position + new Vector3 (0, 2, -5);
		cam.transform.LookAt (transform);
		
		if (myjoystick.JoystickTouch != Vector2.zero && !isAttack)
		{
			if (!isRun)
			{
				ani.SetTrigger("TriggerRun");
				isRun = true;
				
			}
			
			if (isRun) {
				float x = myjoystick.JoystickTouch.x;
				float z = myjoystick.JoystickTouch.y;
				transform.LookAt(new Vector3(x * 10000, transform.position.y, z * 10000));

				m_direction = new Vector3(x, 0, z);
				//m_direction = transform.TransformDirection (m_direction);  
				
				m_direction.y -= m_gravity;  //下降
				m_ctr.Move(m_direction * 5.0f * Time.deltaTime);  //移动方向
				
			}
			
		}
		else
		{
			if (isRun&& !isAttack)
			{
				ani.SetTrigger("TriggerIdle");
				
			}
			isRun = false;
		}
		
		
	}
	
	public void StateJudge(){
		if (!_info.IsName("Idle") && !_info.IsName("Run")&& _info.normalizedTime>0.8f){
			
			_state = State.Idle;
		}
		if (_info.IsName("Attack1") && _hitCount==1 && _info.normalizedTime>0.8f)
		{
			_state = State.Attack1;
			
		}
		if (_info.IsName("Attack2") && _hitCount==2 && _info.normalizedTime>0.8f)
		{
			_state = State.Attack2;
			
		}
		if (_info.IsName("Attack3") && _hitCount==3 &&_info.normalizedTime>0.8f)
		{
			_state = State.Attack3;
		}
	}
	
	public void Attack1(){
		//ani.SetTrigger ("Attack1");
		if (_state==State.Idle)
		{
			ani.SetTrigger("Attack1");
			isAttack = true;
			_hitCount = 1;
		}else if (_state==State.Attack1)
		{
			ani.SetTrigger("Attack1");
			isAttack = true;
			_hitCount = 2;
		}else if (_state==State.Attack2)
		{
			ani.SetTrigger("Attack1");
			isAttack = true;
			_hitCount = 3;
		}
		
	}
	
	
	public void Attack2(){
		ani.SetTrigger ("Attack2");
		isAttack = true;
		GameObject obj=Instantiate (jn1, this.transform.position + transform.forward*2, transform.rotation)as GameObject;
		Destroy (obj,2f);
	}
	public void Attack3(){
		ani.SetTrigger ("Attack3");
		isAttack = true;
		GameObject obj=Instantiate (jn2, this.transform.position + transform.forward, transform.rotation) as GameObject;
		
		Destroy (obj, 2f);
		
	}
	public void Baoji(){
		ani.SetTrigger ("TriggerBaoji");
		isAttack = true;
		GameObject obj=Instantiate (jn3, this.transform.position + transform.forward, transform.rotation)as GameObject;
		Destroy (obj, 2f);
		
	}
	
	//public void chongsheng(){
	//    Application.LoadLevel (1);
	//}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log (other.name);
		if (Hp > 0) {
			if (other.tag.Equals ("guaiwuwuqi")) {
				Hp -= 10f;
			}
		}

		if (Hp <= 0) {
			Hp = 0;
//			Destroy(this.gameObject);
		}
	}

	void updatePercent(){
		slider.value = Hp / maxHp;
	}
	
}
