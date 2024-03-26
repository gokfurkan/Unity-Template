using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Template.Scripts
{
    public class AlphabeticalSorter : MonoBehaviour
    {
        [Button]
        private void SortChildren()
        {
            Transform parentObject = transform;
            
            Transform[] children = new Transform[parentObject.childCount];
            for (int i = 0; i < parentObject.childCount; i++)
            {
                children[i] = parentObject.GetChild(i);
            }
            
            Transform[] sortedChildren = children.OrderBy(child => child.name).ToArray();
            
            for (int i = 0; i < sortedChildren.Length; i++)
            {
                sortedChildren[i].SetSiblingIndex(i);
            }
        }
    }
}