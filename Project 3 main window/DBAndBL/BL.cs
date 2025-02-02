﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Project_3
{
    public class BL
    {
        public BL() { }
        public static bool CheckLogin(string login)
        {
            User user = null;
            using (UserContext userContext = new UserContext()) 
            {   

                user= userContext.Users.Where(u=>u.login==login).FirstOrDefault();
            }
            if (user != null)
            {
                return true;
            }
            return false;
        }
        public static void Registration(string login,string password)
        {
            using (UserContext userContext = new UserContext())
            {
                User user = new User();
                user.login = login;
                user.password = password;
                userContext.Users.Add(user);
                userContext.SaveChanges();
            }
            
        }
        public static int LogIn(string login,string password)
        {
            User user = null;
            using (UserContext userContext=new UserContext())
            {

                user = userContext.Users.Where(u => u.login == login && u.password==password).FirstOrDefault();
            }
            if (user != null)
            {
                return user.id;
            }
            return 0;
        }
        public static void AddBoard(string boardName,int boardType,int userId)
        {
            int boardId;
            using (BoardContext boardContext = new BoardContext())
            {
                Board board = new Board();
                board.name= boardName;
                board.type= boardType;
                boardContext.Boards.Add(board);
                boardContext.SaveChanges();
                boardId = boardContext.Boards.ToArray()[boardContext.Boards.Count()-1].id;
            }
            AddBoardOwner(userId, boardId);

        }
        public static Board GetBoard(int boardId)
        {
            Board board=null;
            using (BoardContext boardContext = new BoardContext())
            {
                board = boardContext.Boards.Where(b => b.id == boardId).FirstOrDefault();
            }
            return board;
        }
        public static void AddBoardOwner(int userId,int boardId)
        {
            using(BoardOwnerContext boardOwnerContext=new BoardOwnerContext())
            {
                BoardOwner boardOwner=new BoardOwner();
                boardOwner.userId= userId;
                boardOwner.boardId= boardId;
                boardOwnerContext.BoardsOwners.Add(boardOwner);
                boardOwnerContext.SaveChanges();
            }
        }
        public static Board[] GetMyBoards(int userId)
        {
            List<Board> boards = new List<Board>();
            List<BoardOwner> boardOwners = new List<BoardOwner>();
            using (BoardOwnerContext boardOwnerContext = new BoardOwnerContext())
            {
                var bo = boardOwnerContext.BoardsOwners.Where(bo => bo.userId == userId);
                boardOwners = bo.ToList();


            }
            using (BoardContext boardContext = new BoardContext())
            {
                foreach (var obj in boardOwners)
                {
                    var b = boardContext.Boards.Where(b => b.id == obj.boardId).FirstOrDefault();
                    boards.Add(b);
                }
            }
            return boards.ToArray();
        }
        public static void RemoveBoard(int boardId)
        {
            using (BoardOwnerContext boardOwnerContext = new BoardOwnerContext())
            {
                var llo= boardOwnerContext.BoardsOwners.Where(bo => bo.boardId == boardId);
                foreach (var he in llo)
                {
                    boardOwnerContext.BoardsOwners.Remove(he);
                }
                
                boardOwnerContext.SaveChanges();


            }
            using (BoardContext boardContext = new BoardContext())
            {
                var llo = boardContext.Boards.Where(b => b.id == boardId);
                foreach (var he in llo)
                {
                    boardContext.Boards.Remove(he);
                }

                boardContext.SaveChanges();
            }

        }
    }
}
