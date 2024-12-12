namespace ClientRenderSwitching.Client;

public static class ClientRenderMode
{
    public static bool ClientRendering => !ServerRendering;
#if DEBUG
    public static bool ServerRendering => true; // true for rendering "client" using server rendering
#else
    public static bool ServerRendering => false;
#endif
}