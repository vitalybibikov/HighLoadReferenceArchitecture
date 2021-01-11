using System;
using System.Collections.Generic;
using Api.Core.Shared.DomainExceptions;
using Api.Core.Shared.Enums;
using Api.Core.Shared.ErrorCodes;

namespace Api.DomainModels
{
    public class Hockey: Competition
    {
        public Hockey(string name, string place, ICollection<Team> teams, DateTime startDate) 
            : base(name, place, teams, startDate, SportType.Hockey)
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
