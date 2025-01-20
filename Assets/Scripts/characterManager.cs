using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class characterManager : MonoBehaviour
{
    public List<Character> charList = new List<Character>();

    public PlayerManager pm;
    public struct Character{
        private string _name;
        private float _moveSpeed; 
        private int _jumpCount;
        private float _jumpForce;
        private float _jumpCut;
        private float _dashForce;
        

        public Character(string name, float moveSpeed, int jumpCount, float jumpForce, float jumpCut, float dashForce)
        {
            _name = name;
            _moveSpeed = moveSpeed;
            _jumpCount = jumpCount;
            _jumpForce = jumpForce;
            _jumpCut = jumpCut;
            _dashForce = dashForce;
        }

        public float getSpeed()
        {
            return _moveSpeed;
        }

        public int getJumpCount()
        {
            return _jumpCount;
        }

        public float getJumpForce()
        {
            return _jumpForce;
        }

        public float getJumpCut()
        {
            return _jumpCut;
        }
        
        public float getDashForce()
        {
            return _dashForce;
        }
    }

    private void Start()
    {
        charList.Add(new Character("Metal", 4f, 1, 5.5f, 0.95f, 0f)); // 0
        charList.Add(new Character("Water", 7f, 1, 5, 0.95f, 0f)); // 1
        // charList.Add(new Character("Metal", 7, 1, 5, 0.95f, 0f)); // 2
        // charList.Add(new Character("Metal", 7, 1, 5, 0.95f, 0f)); // 3
        // charList.Add(new Character("Metal", 7, 1, 5, 0.95f, 0f)); // 4
        // charList.Add(new Character("Metal", 7, 1, 5, 0.95f, 0f)); // 5
        // charList.Add(new Character("Metal", 7, 1, 5, 0.95f, 0f)); // 6
        // charList.Add(new Character("Metal", 7, 1, 5, 0.95f, 0f)); // 7
        // charList.Add(new Character("Metal", 7, 1, 5, 0.95f, 0f)); // 8
    }

    
}
