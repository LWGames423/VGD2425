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
        private int _moveSpeed; 
        private int _jumpCount;
        private float _jumpForce;
        private float _jumpCut;
        private float _dashForce;
        

        public Character(string name, int moveSpeed, int jumpCount, float jumpForce, float jumpCut, float dashForce)
        {
            _name = name;
            _moveSpeed = moveSpeed;
            _jumpCount = jumpCount;
            _jumpForce = jumpForce;
            _jumpCut = jumpCut;
            _dashForce = dashForce;
        }

        public int getSpeed()
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
        
        charList.Add(new Character("Metal", 7, 1, 5, 0.95f, 0f)); // 0
    }

    
}
