namespace SpaceInvader
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Inicia el juego
            SpaceInvader p = new SpaceInvader();
            DAM.Game.Launch(p);
        }
    }
}