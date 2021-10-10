namespace Game.Scripts.Map
{
    public static class MapTileViewFactory
    {
        public static MapTileView Create(MapTileData tileData)
        {
            MapTileView view = new MapTileView(tileData);
            return view;
        }

        public static void Release(MapTileView view)
        {
            
        }
    }
}