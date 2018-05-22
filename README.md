# ReFreegal

### Freegal downloaded file renamer.

Freegal is a free, legal music download service available through many public libraries.

The music files downloaded through Freegal are formatted like this: 
> TrevorMorris_BattleOfBrothers_G010003148378y_1_2-256K_44S_2C_cbr1x.mp3
  TrevorMorris_BjornsChoice_G010003148378y_1_3-256K_44S_2C_cbr1x.mp3
  TrevorMorris_RagnarSaysGoodbyeToG_G010003148378y_1_6-256K_44S_2C_cbr1x.mp3
  TrevorMorris_RollosTrial_G010003148378y_1_5-256K_44S_2C_cbr1x.mp3
  TrevorMorris_VikingsReturnHome_G010003148378y_1_4-256K_44S_2C_cbr1x.mp3
  TrevorMorris_WarIsComing_G010003148378y_1_1-256K_44S_2C_cbr1x.mp3

This is not how I wanted my files named. **ReFreegal** parses the Freegal-created filenames and reads the ID3 tag in the files to produce a more readable name. For example: 
> 01 War Is Coming.mp3
  02 Battle of Brothers.mp3
  03 Bjorn's Choice.mp3
  04 Vikings Return Home.mp3
  05 Rollo's Trial.mp3
  06 Ragnar Says Goodbye to Gyda.mp3

Note: The current version actually _copies_ the original files, and renames the copies. (Safety First!) In a future version, I intend to add the ability to specify a different destination to move/rename to, and make deletion/retention of the original files optional. The copied/renamed files maintain the original file timestamps.
