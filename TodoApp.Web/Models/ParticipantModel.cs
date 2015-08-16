namespace TodoApp.Web.Models
{
    public class ParticipantModel
    {
        public ParticipantModel(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }

        public string Name 
        { 
            get
            {
                if (Id == 1)
                {
                    return "Rocco";
                }

                if (Id == 2)
                {
                    return "Steve McQueen";
                }

                if (Id == 3)
                {
                    return "Martha";
                }

                return null;            
            } 
        }
    }
}