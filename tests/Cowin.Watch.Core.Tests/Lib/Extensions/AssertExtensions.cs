using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cowin.Watch.Core.Tests.Lib.Extensions
{
    public static class AssertExtensions
    {

        public static void ActionWasExecuted(this Assert assert, Action<Action> actionSource)
        {
            bool wasExecuted = false;

            Action customAction = () => { wasExecuted = true; };
            actionSource(customAction);

            if (!wasExecuted) throw new AssertFailedException($"{nameof(actionSource)} was not executed!");
        }

        public static void ActionWasNotExecuted(this Assert assert, Action<Action> actionSource)
        {
            bool wasExecuted = false;

            Action customAction = () => { wasExecuted = true; };
            actionSource(customAction);

            if (wasExecuted) throw new AssertFailedException($"{nameof(actionSource)} was executed!");
        }

        public static void ActionWasExecuted<TInput>(this Assert assert, Action<Action<TInput>> actionSource)
        {
            bool wasExecuted = false;

            Action<TInput> customAction = _ => { wasExecuted = true; };
            actionSource(customAction);

            if (!wasExecuted) throw new AssertFailedException($"{nameof(actionSource)} for {typeof(TInput)} was not executed!");
        }

        public static void ActionWasNotExecuted<TInput>(this Assert assert, Action<Action<TInput>> actionSource)
        {
            bool wasExecuted = false;

            Action<TInput> customAction = _ => { wasExecuted = true; };
            actionSource(customAction);

            if (wasExecuted) throw new AssertFailedException($"{nameof(actionSource)} for {typeof(TInput)} was executed!");
        }

        public static void ActionWasExecutedNTime<TInput>(this Assert assert, Action<Action<TInput>> actionSource, int timesExpected)
        {
            int execCount = 0;

            Action<TInput> customAction = _ => { execCount++; };
            actionSource(customAction);

            if (execCount != timesExpected) throw new AssertFailedException($"{nameof(actionSource)} for {typeof(TInput)} was not executed {timesExpected} times. Excepted : {timesExpected}, Actual : {execCount}!");
        }
    }
}
