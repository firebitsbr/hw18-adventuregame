﻿using System.Collections;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine.Experimental.UIElements;

namespace UnityEngine.AdventureGame
{
    public static class StoryEventConditionNode
    {
        public static IEnumerator Execute(GameLogicData.GameLogicGraphNode currentNode)
        {
            //if true return first return value if false return second
            yield return currentNode.GetReturnValue(true ? 0 : 1);
        }

#if UNITY_EDITOR
        public static Node CreateNode(string typeData)
        {
            Node node = new Node();
            node.title = "StoryEventCondition";

            node.capabilities |= Capabilities.Movable;
            Port inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            inputPort.portName = "";
            inputPort.userData = null;
            node.inputContainer.Add(inputPort);

            Port outputPort1 = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
            outputPort1.portName = "true";
            outputPort1.userData = null;
            node.outputContainer.Add(outputPort1);

            Port outputPort2 = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
            outputPort2.portName = "false";
            outputPort2.userData = null;
            node.outputContainer.Add(outputPort2);

            var characterName = new TextField()
            {
                multiline = false,
                value = typeData
            };

            node.mainContainer.Insert(1, characterName);

            return node;
        }

        public static string ExtractExtraData(Node node)
        {
            foreach (VisualElement ele in node.mainContainer)
            {
                if (ele is TextField)
                {
                    return (ele as TextField).value;
                }
            }

            return null;
        }
#endif
    }
}