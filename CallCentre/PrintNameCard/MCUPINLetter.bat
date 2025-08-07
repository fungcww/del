REM LPR -S HKSIPPRD1 -P PIN_MACAU C:MCUPINLetter.txt

REM LPR -S HKSIPPRD1 -P PIN_MACAU C:MCUPINLetterhk.txt

MCUPINLetter.exe 

set folderdate=%date:~6,4%-%date:~0,2%-%date:~3,2%
mkdir %folderdate%
COPY C:MCUPINLetter.txt %folderdate%
COPY C:MCUPINLetterHK.txt %folderdate%

del C:MCUPINLetter.txt

del C:MCUPINLetterHK.txt