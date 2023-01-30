#if(UNITY_EDITOR)
using UnityEngine;
#pragma warning disable IDE0090 // Use 'new(...)'
public class ConstraintCopier : MonoBehaviour
{
    [Tooltip("The root of the chain of bones that you want to constrain to something else")]
    public GameObject sourceRoot;
    [Tooltip("The root of the chain of bones that you want something else to be constrained to")]
    public GameObject targetRoot;
    [Tooltip("Which bones in the hierarchy that you want the script to stop when it hits")]
    public Transform[] ignoreTransforms;
    public int constraintType;

    private UnityEngine.Animations.ParentConstraint parentConstraint;
    private UnityEngine.Animations.RotationConstraint rotationConstraint;
    private UnityEngine.Animations.PositionConstraint positionConstraint;

    protected void OnDrawGizmosSelected()
    {
        //Gizmos.DrawCube(new Vector3(0, stepHeight, 0), new Vector3(3, 0.0001f, 3));
    }
    public void CreateConstraintRelationship(int constraintType)
    {
        CreateConstraintAtBone(this.sourceRoot, this.targetRoot, constraintType);
    }

    private void CreateConstraintAtBone(GameObject sourceRoot, GameObject targetRoot,int constraintType)
    {
        if (constraintType == 0) // Parent Constraint
        {
            parentConstraint = sourceRoot.AddComponent<UnityEngine.Animations.ParentConstraint>();
            parentConstraint.AddSource(new UnityEngine.Animations.ConstraintSource { weight = 1, sourceTransform = targetRoot.transform });
            parentConstraint.constraintActive = true;
            parentConstraint.locked = true;
        }
        else if (constraintType == 1) // Rotation Constraint
        {
            rotationConstraint = sourceRoot.AddComponent<UnityEngine.Animations.RotationConstraint>();
            rotationConstraint.AddSource(new UnityEngine.Animations.ConstraintSource { weight = 1, sourceTransform = targetRoot.transform });
            rotationConstraint.constraintActive = true;
            rotationConstraint.locked = true;
        }
        else // Position constraint
        {
            positionConstraint = sourceRoot.AddComponent<UnityEngine.Animations.PositionConstraint>();
            positionConstraint.AddSource(new UnityEngine.Animations.ConstraintSource { weight = 1, sourceTransform = targetRoot.transform });
            positionConstraint.constraintActive = true;
            positionConstraint.locked = true;
        }
        for (int i = 0; i < sourceRoot.transform.childCount; i++)
        {
            CreateConstraintAtBone(sourceRoot.transform.GetChild(i).gameObject, targetRoot.transform.GetChild(i).gameObject, constraintType);
        }
    }
}
#endif 