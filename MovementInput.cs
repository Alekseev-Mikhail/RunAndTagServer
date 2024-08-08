namespace RunAndTagServer;

public readonly struct MovementInput(byte inputIndex, byte up, byte down, byte left, byte right)
{
    public byte InputIndex => inputIndex;
    public byte Up => up;
    public byte Down => down;
    public byte Left => left;
    public byte Right => right;
}