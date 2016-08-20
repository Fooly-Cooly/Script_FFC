echo("\nFFC - Loading required Add-On...");
if(!$AddOn__Vehicle_Horse)
	exec("Add-Ons/Vehicle_Horse/server.cs");
if(!$AddOn__Vehicle_Tank)
	exec("Add-Ons/Vehicle_Tank/server.cs");

echo("\n--------- Initializing FFC Server ---------");
exec("./server/ffc_animation.cs");
exec("./server/ffc_explosion.cs");
exec("./server/ffc_mount.cs");
exec("./server/ffc_nodes.cs");
exec("./server/ffc_normal.cs");
exec("./server/ffc_resize.cs");
exec("./server/ffc_transform.cs");
exec("./server/ffc_servercmds.cs");
exec("./server/ffc_support.cs");

echo("\n--------- Loading FFC Datablocks ----------");
exec("./content/head/ffc_hats.cs");
exec("./content/head/ffc_heads.cs");
exec("./content/player/ffc_dog.cs");
exec("./content/player/ffc_monkey.cs");
if(isFile("base/data/shapes/player/minifig.dts"))
	exec("./content/player/ffc_minifig.cs");
//if(isFile("base/data/shapes/player/mBeta.dts"))
//	exec("./content/player/ffc_beta.cs");