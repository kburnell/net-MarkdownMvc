# Markdown With Conditionals #


## Should See Stuff ##
||??shouldSeeThis??||
Should See This - This is some conditional content with no replacement
||??shouldSeeThis??||

||??shouldSeeThis??||
Should See See This - Conditional content with replacements:
- [||emailAddressGeneral||](mailto:||EmailAddressGeneral||)
- <a href="||facebookUrl||" target="_blank">That Conference on Facebook</a>
||??shouldSeeThis??||

## Should NOT See Stuff ##
||??shouldNotSeeThis??||
Should NOT See This - This is some conditional content with no replacement
||??shouldNotSeeThis??||

||??shouldNotSeeThis??||
Should NOT See This - Conditional content with replacements:
- [||emailAddressGeneral||](mailto:||EmailAddressGeneral||)
- <a href="||facebookUrl||" target="_blank">That Conference on Facebook</a>
||??shouldNotSeeThis??||


## Should See Stuff ##
||?!shouldNotSeeThis!?||
Should See This: Negated conditional content with no replacement
||?!shouldNotSeeThis!?||

||?!shouldNotSeeThis!?||
Should See This: Negated conditional content with replacements:
- <a href="||publicSlackUrl||" target="_blank">That Conference on Slack</a>
- <a href="||twitterUrl||" target="_blank">That Conference on Twitter</a>
||?!shouldNotSeeThis!?||


## Should NOT See Stuff ##
||?!shouldSeeThis!?||
Should NOT See This: Negated conditional content with no replacement
||?!shouldSeeThis!?||

||?!shouldSeeThis!?||
Should NOT See This: Negated conditional content with replacements:
- <a href="||publicSlackUrl||" target="_blank">That Conference on Slack</a>
- <a href="||twitterUrl||" target="_blank">That Conference on Twitter</a>
||?!shouldSeeThis!?||

