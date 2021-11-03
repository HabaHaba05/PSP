## PasswordValidator Tests:
#### Reikalavimas: Tikrina ar slaptažodžio ilgis netrumpesnis nei X
Dėja bet šiuose testuose nepaduodamas joks X
Galbūt mūsų reikalivimas turėti slaptažodį tiesiog ilgesnį už 1 ir šiuo atveju GivenPasswordValidator_WhenPasswordIsTooShort_ReturnsFalse FAILINA
Tas pats galioja su kitais testais jeigu mes norime turėti labai ilgą passworda pvz. X=30

#### Reikalavimas: Tikrina ar yra specialus simbolis (specialių simbolių sąrašas turi būti konfiguruojamas)
Taip pat nepaduodamas joks specialių simbolių sąrašas - tos pačios problemos kaip ir su ilgiu
GivenPasswordValidator_WhenPasswordContainsInvalidSpecialSymbol_ReturnsTrue - Nežinau kas buvo norėta pasakyti su InvalidSpecialSymbol ir logiškai mąstant kodėl jeigu passworde yra kažkoks tai Invalid simbolis jis gražina True? Testo nepanaikinu tiesiog jį Skipin'u

##### Kita
GivenPasswordValidator_WhenPasswordDoesntHaveDigit_ReturnsFalse() Tokio reikalavimo nebuvo, testo nepanaikinu tiesiog jį Skipin'u

## PhoneValidator Tests:
#### Reikalavimas: Jei prasideda su 8, tai pakeičia į +370
GivenNumberValidator_WhenNumberStartsWith8_ReturnsTrue - Mano nuomone čia nereikia jokio tikrinimo ar True ar False, tiesiog reikia patikrinti ar pakeičiamas stringas.

#### Reikalavimas: Yra galimybė pridėti naujų validavimo taisyklių pagal šalį (ilgis ir prefiksas)
Visiškai nieko panašaus į tai nematau, taigi panaikinau testus :
GivenNumberValidator_WhenNumberDoesntStartWithPlus_ReturnsFalse()
GivenNumberValidator_WhenNumberCodeIsFromConfig_ReturnsTrue()
GivenNumberValidator_WhenNumberIsTooShort_ReturnsFalse()
GivenNumberValidator_WhenNumberStartsWithInvalidCode_ReturnsFalse()
GivenNumberValidator_WhenNumberIsTooLong_ReturnsFalse()

Pridėjau testus:
GivenPhoneValidator_WhenLengthAndPrefixAreTheSame_AsSpecifiedInTheRule_ReturnsTrue
GivenPhoneValidator_WhenPrefixIsDifferent_ThanSpecifiedInTheRule_ReturnsFalse
GivenPhoneValidator_WhenLengthIsLongerOrShorter_ThanSpecifiedInTheRule_ReturnsFalse

## EmailValidator Tests:
Lietuviški simboliai laikomi kaip Invalid, dėlto reikėjo man susikurti Extension metod'ą IsLetter() kur tikrinama tik aA-zZ
GivenEmailValidator_WhenEmailDomainIsCorrect_ReturnsTrue() - Buvo assertinama kad false