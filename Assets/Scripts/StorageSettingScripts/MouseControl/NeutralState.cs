namespace StorageSettingScripts.MouseControl
{
public class NeutralState : IMouseState
{
    public IMouseState GetState()
    {
        return this;
    }

    public void Update()
    {
    }
    

}
}