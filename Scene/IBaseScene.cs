public interface IBaseScene
{
    // public string sceneName; // 场景名称
    public void RegisterScene();
    public void RegisterNPC(BaseNPC npc); // 登记当前交互的NPC
    public void ShowPanelDialog();
    public void HidePanelDialog();
    public void ShowPanelBag();
    public void HidePanelBag();
    public void ShowPanelKeyboard();
    public void HidePanelKeyboard();
    public void ShowPanelUsage();
    // public void ShowOptions();
    // public void HideOptions();
}
