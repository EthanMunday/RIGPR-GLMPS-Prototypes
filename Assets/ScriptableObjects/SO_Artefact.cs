using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Artefact", menuName = "Artefacts/New Artefact", order = 1)]
public class SO_ArtefactBase : ScriptableObject
{
    // This is just a demo of the stuff, decisions come later

    // Generic Artefact Stuff
    public string artefactName;
    public string artefactDescription;
    public string artefactType;
    public float valueOfArtefact;
    public float revenueGenerated;
    public string[] artefactCurseList;

    // Mesh Stuff
    public Mesh artefactMesh;
    public Material artefactMaterial;

    // TRS Stuff
    public Vector3 objectPositionOffset;
    public Vector3 objectScaleSize;
    public Vector3 objectRotation;

    // Collider Stuff
    public Vector3 collisionSize;
    public Vector3 collisionScale;
}
