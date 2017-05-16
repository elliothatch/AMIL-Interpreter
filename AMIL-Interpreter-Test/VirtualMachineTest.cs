using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;

namespace AmilTest
{
    [TestClass]
    public class VirtualMachineTest
    {
        [TestMethod]
        public void VirtualMachineTest1()
        {
            Amil.Lexer lexer = Amil.Amil.AmilLexer;
            Amil.Parser parser = new Amil.Parser();
            Amil.VirtualMachine vm = new Amil.VirtualMachine(100);
            vm.LoadInstructions(parser.parse(lexer.Lex("{print 1; print 2*3-4+5; print true; }")).Compile());
            vm.Execute();
        }

        [TestMethod]
        public void VirtualMachineTest2()
        {
            Amil.Lexer lexer = Amil.Amil.AmilLexer;
            Amil.Parser parser = new Amil.Parser();
            Amil.VirtualMachine vm = new Amil.VirtualMachine(100);
            vm.LoadInstructions(parser.parse(lexer.Lex("{if false { print false; } print true; }")).Compile());
            vm.Execute();
        }
    }
}
