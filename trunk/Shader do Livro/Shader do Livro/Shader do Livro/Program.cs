using System;

namespace Shader_do_Livro
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game3 game = new Game3())
            {
                game.Run();
            }
        }
    }
#endif
}

