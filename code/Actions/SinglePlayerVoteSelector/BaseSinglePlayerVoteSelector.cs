using Sandbox;
using System.Collections;
using System.Collections.Generic;
using TerryTrials.Player;

namespace TerryTrials.Actions.SinglePlayerVoteSelector;
public partial class BaseSinglePlayerVoteSelector : BaseAction
{
	private protected Dictionary<MafiaPlayer, MafiaPlayer> Votes; // <MafiaPlayer VOTER, MafiaPlayer VOTEE>

	public virtual void CastVote(MafiaPlayer onPlayer, MafiaPlayer votingPlayer)
	{
		Votes[votingPlayer] = onPlayer;
	}

	public virtual Dictionary<MafiaPlayer, MafiaPlayer> GetVoteTable()
	{
		return Votes;
	}
}
