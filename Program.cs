using CSAssembler;

Console.Write("Input path to .asm file: ");
string? path = Console.ReadLine();
if (path != null)
{
    if (File.Exists(path))
    {
        AssemblerHack assm = new AssemblerHack(path);
        assm.firstPass();
        assm.secondPass();
    }
    else
    {
        throw new Exception("couldn't find file");
    }
}
else
{
    throw new Exception("path to file must be string, not null");
}
