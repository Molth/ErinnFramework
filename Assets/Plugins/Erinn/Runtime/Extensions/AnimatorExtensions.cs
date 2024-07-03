//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace Erinn
{
    public static partial class UnityExtensions
    {
        public static float GetCurrentAnimatorStateLength(this Animator self) => self.GetCurrentAnimatorStateInfo(0).length;

        public static float GetCurrentAnimatorStateLength(this Animator self, int layerIndex) => self.GetCurrentAnimatorStateInfo(layerIndex).length;

        public static bool HasParameterOfType(this Animator self, string name, AnimatorControllerParameterType type)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            foreach (var parameter in self.parameters)
                if (parameter.type == type && parameter.name == name)
                    return true;
            return false;
        }

        public static void AddAnimatorParameterIfExists(this Animator animator, string parameterName, out int parameter, AnimatorControllerParameterType type, HashSet<int> parameterList)
        {
            if (string.IsNullOrEmpty(parameterName))
            {
                parameter = -1;
            }
            else
            {
                parameter = Animator.StringToHash(parameterName);
                if (!animator.HasParameterOfType(parameterName, type))
                    return;
                parameterList.Add(parameter);
            }
        }

        public static void AddAnimatorParameterIfExists(this Animator animator, string parameterName, AnimatorControllerParameterType type, HashSet<string> parameterList)
        {
            if (!animator.HasParameterOfType(parameterName, type))
                return;
            parameterList.Add(parameterName);
        }

        public static void UpdateAnimatorBool(this Animator animator, string parameterName, bool value) => animator.SetBool(parameterName, value);

        public static void UpdateAnimatorInteger(this Animator animator, string parameterName, int value) => animator.SetInteger(parameterName, value);

        public static void UpdateAnimatorFloat(this Animator animator, string parameterName, float value) => animator.SetFloat(parameterName, value);

        public static bool UpdateAnimatorBool(this Animator animator, int parameter, bool value, HashSet<int> parameterList, bool performSanityCheck = true)
        {
            if (performSanityCheck && !parameterList.Contains(parameter))
                return false;
            animator.SetBool(parameter, value);
            return true;
        }

        public static bool UpdateAnimatorTrigger(this Animator animator, int parameter, HashSet<int> parameterList, bool performSanityCheck = true)
        {
            if (performSanityCheck && !parameterList.Contains(parameter))
                return false;
            animator.SetTrigger(parameter);
            return true;
        }

        public static bool SetAnimatorTrigger(this Animator animator, int parameter, HashSet<int> parameterList, bool performSanityCheck = true)
        {
            if (performSanityCheck && !parameterList.Contains(parameter))
                return false;
            animator.SetTrigger(parameter);
            return true;
        }

        public static bool UpdateAnimatorFloat(this Animator animator, int parameter, float value, HashSet<int> parameterList, bool performSanityCheck = true)
        {
            if (performSanityCheck && !parameterList.Contains(parameter))
                return false;
            animator.SetFloat(parameter, value);
            return true;
        }

        public static bool UpdateAnimatorInteger(this Animator animator, int parameter, int value, HashSet<int> parameterList, bool performSanityCheck = true)
        {
            if (performSanityCheck && !parameterList.Contains(parameter))
                return false;
            animator.SetInteger(parameter, value);
            return true;
        }

        public static void UpdateAnimatorBool(this Animator animator, string parameterName, bool value, HashSet<string> parameterList)
        {
            if (!parameterList.Contains(parameterName))
                return;
            animator.SetBool(parameterName, value);
        }

        public static void UpdateAnimatorTrigger(this Animator animator, string parameterName, HashSet<string> parameterList)
        {
            if (!parameterList.Contains(parameterName))
                return;
            animator.SetTrigger(parameterName);
        }

        public static void SetAnimatorTrigger(this Animator animator, string parameterName, HashSet<string> parameterList)
        {
            if (!parameterList.Contains(parameterName))
                return;
            animator.SetTrigger(parameterName);
        }

        public static void UpdateAnimatorFloat(this Animator animator, string parameterName, float value, HashSet<string> parameterList)
        {
            if (!parameterList.Contains(parameterName))
                return;
            animator.SetFloat(parameterName, value);
        }

        public static void UpdateAnimatorInteger(this Animator animator, string parameterName, int value, HashSet<string> parameterList)
        {
            if (!parameterList.Contains(parameterName))
                return;
            animator.SetInteger(parameterName, value);
        }

        public static void UpdateAnimatorBoolIfExists(this Animator animator, string parameterName, bool value)
        {
            if (!animator.HasParameterOfType(parameterName, AnimatorControllerParameterType.Bool))
                return;
            animator.SetBool(parameterName, value);
        }

        public static void UpdateAnimatorTriggerIfExists(this Animator animator, string parameterName)
        {
            if (!animator.HasParameterOfType(parameterName, AnimatorControllerParameterType.Trigger))
                return;
            animator.SetTrigger(parameterName);
        }

        public static void SetAnimatorTriggerIfExists(this Animator animator, string parameterName)
        {
            if (!animator.HasParameterOfType(parameterName, AnimatorControllerParameterType.Trigger))
                return;
            animator.SetTrigger(parameterName);
        }

        public static void UpdateAnimatorFloatIfExists(this Animator animator, string parameterName, float value)
        {
            if (!animator.HasParameterOfType(parameterName, AnimatorControllerParameterType.Float))
                return;
            animator.SetFloat(parameterName, value);
        }

        public static void UpdateAnimatorIntegerIfExists(this Animator animator, string parameterName, int value)
        {
            if (!animator.HasParameterOfType(parameterName, AnimatorControllerParameterType.Int))
                return;
            animator.SetInteger(parameterName, value);
        }
    }
}