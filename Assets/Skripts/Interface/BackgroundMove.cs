using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackgroundMove : MonoBehaviour {

	public RectTransform _cursor;
	public float sensitivity;
	public float speed;
	public Sprite arrow;

	private Quaternion current;
	private Sprite current_img;
	private Vector3 direction, directionS;
	private float X, Y;
	private bool firstChek = true;

	void Start()
	{
		Cursor.visible = false;
		current = _cursor.rotation;
	}

	void LateUpdate()
	{
		X = 0;
		Y = 0;
		if (CheckMouse ()) 
			transform.Translate (direction * speed * Time.deltaTime);
		if (ChekSens ())
		{
			if(firstChek)
				current_img = _cursor.GetComponent<Image>().sprite;
			firstChek = false;
			_cursor.GetComponent<Image>().sprite = arrow;
			float angle = Mathf.Atan2 (directionS.y, directionS.x) * Mathf.Rad2Deg - 115;
			_cursor.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		}
		else 
		{
			if(!firstChek)
				_cursor.GetComponent<Image>().sprite = current_img;
			firstChek = true;
			_cursor.rotation = current;
		}
		_cursor.transform.position = new Vector3 (Input.mousePosition.x + X + 5, Input.mousePosition.y + Y - 10, 0);
	}

    bool ChekLeft()
    {
        //RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(0 - sensitivity, 0, 0)), Vector2.zero);
        //Vector3 v3 = new Vector3(transform.position.x - sensitivity - Screen.width / 2, Screen.height / 2, 0);
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(-sensitivity, 0, 0));
        RaycastHit2D hitInfo = Physics2D.GetRayIntersection(ray);
        if (hitInfo)
            return true;
        return false;
    }

    bool ChekRight()
    {
        //RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + sensitivity, 0, 0)), Vector2.zero);
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width + sensitivity, 0, 0));
        RaycastHit2D hitInfo = Physics2D.GetRayIntersection(ray);
        if (hitInfo)
            return true;
        return false;
    }

    bool ChekBot()
    {
        //RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(0, 0 - sensitivity, 0)), Vector2.zero);
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(0, -sensitivity, 0));
        RaycastHit2D hitInfo = Physics2D.GetRayIntersection(ray);
        if (hitInfo)
            return true;
        return false;
    }

    bool ChekTop()
    {
        //RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height + sensitivity, 0)), Vector2.zero);
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(0, Screen.height + sensitivity, 0));
        RaycastHit2D hitInfo = Physics2D.GetRayIntersection(ray);
        if (hitInfo)
            return true;
        return false;
    }
	
	bool CheckMouse()
	{
		bool left = false, right = false, down = false, up = false;

        if (Input.mousePosition.x < sensitivity && ChekLeft()) 
		{ 
			left = true;
			direction = Vector3.left;
		}

        if (Input.mousePosition.x > Screen.width - sensitivity && ChekRight()) 
		{
			right = true;
			direction = Vector3.right;
		}

        if (Input.mousePosition.y < sensitivity && ChekBot())
		{
			down = true;
			direction = Vector3.down;
		}

        if (Input.mousePosition.y > Screen.height - sensitivity && ChekTop())
		{
			up = true;
			direction = Vector3.up;
		}

		if(left && up)
		{
			direction = new Vector3(-1, 1, 0);
			return true;
		}
		else if(left && down)
		{
			direction = new Vector3(-1, -1, 0);
			return true;
		}
		else if(right && down)
		{
			direction = new Vector3(1, -1, 0);
			return true;
		}
		else if(right && up)
		{
			direction = new Vector3(1, 1, 0);
			return true;
		}
		
		if(left || up || right || down)
			return true;
		
		return false;
	}

	bool ChekSens()
	{
		bool left = false, right = false, down = false, up = false;
		
		if (Input.mousePosition.x < sensitivity) 
		{ 
			left = true;
			directionS = Vector3.left;
			X = _cursor.rect.width/2 + Input.mousePosition.x * -1 - 5;
		}
		
		if (Input.mousePosition.x > Screen.width - sensitivity) 
		{
			right = true;
			directionS = Vector3.right;
			X = (_cursor.rect.width/2 + Input.mousePosition.x - Screen.width) * -1 - 5;
		}
		
		if(Input.mousePosition.y < sensitivity)
		{
			down = true;
			directionS = Vector3.down;
			Y = _cursor.rect.width/2 + Input.mousePosition.y * -1 + 10;
		}
		
		if(Input.mousePosition.y > Screen.height - sensitivity)
		{
			up = true;
			directionS = Vector3.up;
			Y = (_cursor.rect.width/2 + Input.mousePosition.y - Screen.height) * -1 + 10;
		}
		
		if(left && up)
		{
			directionS = new Vector3(-1, 1, 0);
			return true;
		}
		else if(left && down)
		{
			directionS = new Vector3(-1, -1, 0);
			return true;
		}
		else if(right && down)
		{
			directionS = new Vector3(1, -1, 0);
			return true;
		}
		else if(right && up)
		{
			directionS = new Vector3(1, 1, 0);
			return true;
		}
		
		if(left || up || right || down)
			return true;
		
		return false;
	}
}
