datablock ShapeBaseImageData(GhostImage)
{
	shapeFile = "./head_ghost.dts";
	emap = false;
	mountPoint = $HeadSlot;
	offset = "0 0 0";
	eyeOffset = "-1 -1 -1";
};

datablock ShapeBaseImageData(PBearImage)
{
	shapeFile = "./head_pedo.dts";
	emap = true;
	doColorShift = true;
	colorshiftcolor = "0.4 0.3 0.2 1";
	mountPoint = $HeadSlot;
	offset = "0 -0.04 -0.14";
	eyeOffset = "-1 -1 -1";
	rotation = "0 0 0 0";
	className = "ItemImage";

};

datablock ShapeBaseImageData(SkeleHeadImage)
{
	shapeFile = "./head_skele.dts";
	emap = false;
	doColorShift = True;
	colorshiftcolor = "1 1 1 1";
	mountPoint = 5;
	offset = "0 0 -.4";
	eyeOffset = "-1 -1 -1";

};

FFC_AddHead("Bear", "Head", "PBearImage");
FFC_AddHead("Ghost", "Head", "GhostImage");
FFC_AddHead("Skele", "Head", "SkeleHeadImage");