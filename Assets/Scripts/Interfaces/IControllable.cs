namespace Game.Interfaces
{
    public interface IControllable
    {
        bool jump {get; set; }
        bool moveLeft {get;set;}
        bool moveRight {get;set;}
        bool moveUp {get;set;}
        bool moveDown {get;set;}
        bool turbo {get;set;}
        bool secondJump {get;set;}
    }
}

