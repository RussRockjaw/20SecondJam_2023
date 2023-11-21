public struct Settings
{
    public int time;
    public int width;
    public int height;
    public int maxPieceSize;

    public Settings(int time, int width, int height, int maxPieceSize)
    {
        this.time = time;
        this.width = width;
        this.height = height;
        this.maxPieceSize = maxPieceSize;
    }

    public Settings Copy()
    {
        return new Settings(time, width, height, maxPieceSize);
    }
}


