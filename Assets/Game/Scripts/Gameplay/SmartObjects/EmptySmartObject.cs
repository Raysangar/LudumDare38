public class EmptySmartObject : SmartObject
{
  public override void Interact ()
  {
    base.Interact ();
    print ("Interaction");
  }
}
