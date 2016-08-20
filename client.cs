echo("\n--------- Initializing FFC Client ---------");
exec("./client/ffc_minigame.gui");
if($pref::FFC::Minifig != 1)
	exec("./client/ffc_download.cs");

package FFC_Client
{
	function CreateMiniGameGui::clickCreate()
	{
		Parent::ClickCreate();
		%info0 = $MiniGame::Cloak SPC $MiniGame::FloatingHead SPC $MiniGame::Horse SPC $MiniGame::Monkey;
		%info1 = $MiniGame::PlayDead SPC $MiniGame::Resize SPC $Minigame::Skeleton SPC $MiniGame::Smurf;
		%info2 = $MiniGame::Dog;
		commandToServer('FFCSetMiniGameData',%info0,%info1,%info2);
	}
};
ActivatePackage(FFC_Client);