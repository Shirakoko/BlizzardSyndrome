using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 储存一个小篇章/Panel中一段对话的信息的类
/// </summary>
public class DialogInfo
{
    public List<OneWord> dialogs;

}

/// <summary>
/// 储存一句话的信息类
/// </summary>
public class OneWord
{
    public string charaName;
    public string charaWord;
    public bool isTriggerOptionPanel; // 是否触发选项分支面板
}

public class OptionInfo
{
    public List<OptionSet> options;
}

public class OptionSet
{
    public string option_1;
    public string option_2;
}
