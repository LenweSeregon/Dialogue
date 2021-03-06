﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC_Speaker", menuName = "NPC_Speaker/Discussion/NoQuest")]
public class NPC_Discussion_Scriptable_Object : ScriptableObject
{
    public string title;
    public bool isQuestAttached = false;
    public bool isFinalQuestDiscussion = false;
    [TextArea]
    public List<string> content;
    public List<NPC_Discussion_Scriptable_Object> relatedDiscussions;
}
