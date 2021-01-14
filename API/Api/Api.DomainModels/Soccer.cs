using System;
using System.Collections.Generic;
using Api.Core.Shared.DomainExceptions;
using Api.Core.Shared.Enums;
using Api.Core.Shared.ErrorCodes;

namespace Api.DomainModels
{
    public class Soccer: Competition
    {
        public Soccer(string name, string place, ICollection<Team> teams, DateTime startDate, string uniqueId) 
            : base(name, place, teams, startDate, SportType.Soccer, uniqueId)
        {
            if (teams == null || teams.Count != 2)
            {
                throw new DomainException("Wrong Teams Count", DomainErrorCode.TEAMS_NUMBER_INCORRECT);
            }
        }

        //and other business rules, related to a specified game type should be validated below.
        public override void FinishGame(DateTime endDateTime)
        {
            throw new NotImplementedException();
        }
    }
}
