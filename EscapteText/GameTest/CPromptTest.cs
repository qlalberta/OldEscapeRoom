using System;
using System.Collections.Generic;
using System.Text;
using GameEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameTest
{
    [TestClass]
    public class CPromptTest
    {
        [TestMethod]
        public void InputSingleText()
        {
            CommandPrompt input = new CommandPrompt();
            input.Read("Quinn");
            Assert.AreEqual(input.LastInput, "Quinn");
        }
    }
}

