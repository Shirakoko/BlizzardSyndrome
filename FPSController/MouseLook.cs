using UnityEngine;
using System.Collections;

/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
[AddComponentMenu("Character/Mouse Look")]
public class MouseLook : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public bool isLocked = false; // 是否锁定视角
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    private float rotationX = 0F;
    private float rotationY = 0F;

    private Quaternion originalRotation;

<<<<<<< HEAD
    public void Update()
    {   
        if(Input.GetKeyDown(KeyCode.L)) // 按下L解锁或锁定视角
        {
            isLocked = !isLocked;
        }
        if(isLocked){return;} // 如果视角锁定就直接跳过
=======
    void Start()
    {
        GameManager.Instance.eventCenter.AddEventListener<KeyCode>("KeyDown",(KeyCode key)=>{ // 将lambda表达式写入委托容器
            if(key == KeyCode.L) // 按下L解锁或锁定视角
            {
                isLocked = !isLocked;
            }
            if(isLocked){return;} // 如果视角锁定就直接跳过
        });
        
        if (GetComponent<Rigidbody>()!=null){ GetComponent<Rigidbody>().freezeRotation = true; }
        originalRotation = transform.localRotation;
    }

    void Update()
    {   
        // if(Input.GetKeyDown(KeyCode.L)) // 按下L解锁或锁定视角
        // {
        //     isLocked = !isLocked;
        // }
        if(isLocked){return;} // 如果视角锁定就直接跳过

>>>>>>> 90c4bc3 (1208)
        Quaternion xQuaternion;
        Quaternion yQuaternion;
        if (axes == RotationAxes.MouseXAndY)
        {
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            rotationY = ClampAngle(rotationY, minimumY, maximumY);

            xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);

            transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        }
        else if (axes == RotationAxes.MouseX)
        {
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);

            xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up); // 绕y轴旋转rotationX度
            transform.localRotation = originalRotation * xQuaternion;
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = ClampAngle(rotationY, minimumY, maximumY);

            yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left); // 绕x轴旋转rotationY度
            transform.localRotation = originalRotation * yQuaternion;
        }
    }

<<<<<<< HEAD
    public void Start()
    {
        if (GetComponent<Rigidbody>()!=null){GetComponent<Rigidbody>().freezeRotation = true;}
        originalRotation = transform.localRotation;
    }

=======
>>>>>>> 90c4bc3 (1208)
    public static float ClampAngle(float angle, float min, float max)
    {
        // 一般都会填-360到360之间的角度，为了安全
        if (angle < -360.0f){angle += 360.0f;}
        if (angle > 360.0f){angle -= 360.0f;}   
        return Mathf.Clamp(angle, min, max);
    }
}