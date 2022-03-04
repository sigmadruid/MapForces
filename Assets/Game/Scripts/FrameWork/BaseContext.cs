namespace Game.Scripts.FrameWork
{
    public static class BaseContext
    {
        public static SpawnManager Spawn { get; private set; }
        public static ResourceManager Resource { get; private set; }
        
        public static InputManager Input { get; private set; }
        
        public static void Initialize()
        {
            Spawn = new SpawnManager();
            Spawn.Initialize();
            
            Resource = new ResourceManager();
            Resource.Initialize();
            
            InputConfig inputConfig = Resource.Load<InputConfig>("Assets/Game/ResourceData/Config/input_config.asset");
            Input = new InputManager();
            Input.Initialize(inputConfig);
        }

        public static void Dispose()
        {
            Spawn.Dispose();
            Resource.Dispose();
            Input.Dispose();
        }

        public static void Tick(float deltaTime)
        {
            Resource.Tick(deltaTime);
            Spawn.Tick(deltaTime);
            Input.Tick(deltaTime);
        }
    }
}