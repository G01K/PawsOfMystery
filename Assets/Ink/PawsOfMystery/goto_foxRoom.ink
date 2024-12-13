=== goto_foxRoom ===
~ currentSpeaker = Characters.해설
다스는 족제비와 함께 여우의 방으로 향했다.
~ currentSpeaker = Characters.족제비
여우의 집은 여기야.
~ currentSpeaker = Characters.해설
족제비는 여우의 집 문을 가볍게 두드렸다.  
잠시 후, 문이 삐걱거리며 열렸다.  
방 안은 물건이 아무 곳에나 뒹굴고 있는 걸 보니 정리와는 친하지 않은 것 같다.
~ currentSpeaker = Characters.족제비
안녕 여우야. 여기는 고양이 탐정 '다스'야.  
서로 인사해.
~ currentSpeaker = Characters.여우
뭐야? 누구야, 귀찮게?  
고양이면 고양이지, 고양이 탐정이 뭐야?!
~ currentSpeaker = Characters.주인공
(성격이 괴팍해 보인다쭈.)  
크흠, 나는 고양이 탐정 '다쭈'다쭈!  
냄새를 잘 맡아서 몇 가지 사건을 해결해줬더니 탐정이라고 불리게 되었다쭈.  
이번 사건도 그런 셈이다쭈.
~ currentSpeaker = Characters.여우
뭐? 내가 또 뭘 잘못했다고 그래?  
~ currentSpeaker = Characters.주인공
(아직 아무 말도 안 했다쭈! 뭐 찔리는 거라도 있나쭈?)

* 옛말에 도둑이 제 발을 저린다고 했다쭈!
    ~ currentSpeaker = Characters.여우
    어? 아니야. 왜 사람들이 자꾸 나만 의심하는 거지?
    ~ currentSpeaker = Characters.주인공
    실종 사건이다쭈.  
    너는 현장에 증거가 있어서 용의자로 조사하러 왔다쭈.  
    자백하려면 지금도 늦지 않았으니 말해라쭈.  
    -> afterTalkFox

* [킁킁 현장에서 주운 털과 여우의 냄새를 비교해본다]
    ~ currentSpeaker = Characters.주인공
    어제 저녁에 어디 있었냐쭈?  
    ~ currentSpeaker = Characters.여우
    나? 뭐 별거 없었는데? 그냥 산책이나 다녔던 거 같네.
    ~ currentSpeaker = Characters.주인공
    햄스터가 사라졌다쭈. 사실대로 말한다면 목숨만은 살려주겠다쭈.
    -> afterTalkFox

=== afterTalkFox ===
~ currentSpeaker = Characters.여우
햄스터가 사라졌다고? 갑자기 왜?  
~ currentSpeaker = Characters.주인공
마음 짚이는 곳이 있다면 말해봐라쭈. 추리에 도움이 될 거다쭈.  

-> choose_home

=== get_hintApple ===
~ currentSpeaker = Characters.해설
바닥에 뒹굴고 있는 사과가 보인다.  
사과 표면에는 동그란 타원형의 구멍이 가볍게 몇 개씩 있다.  
가까이에서 보니 자국 주위로 껍질이 찢어져 있고 속살이 갈변해 있다.  
~ currentSpeaker = Characters.주인공
이 사과는 도대체 왜 이렇게 됐냐쭈? 먹다가 실수로 떨어뜨린 거냐쭈?
~ currentSpeaker = Characters.여우
...아니, 그냥... 어쩌다 보니 그렇게 됐어. 별거 아냐.
~ currentSpeaker = Characters.해설
여우는 허둥지둥하며 당황해한다.
~ currentSpeaker = Characters.주인공
(별거 아니라니, 뭔가 숨기는 것 같다쭈.)

* 사과 자국에 대해 더 물어본다.
    -> ask_about_apple
* 방이 더러운 이유를 물어본다.
    -> ask_about_mess

=== ask_about_apple ===
~ currentSpeaker = Characters.주인공
이 자국은 분명히 뭔가를 물었다가 생긴 것 같다쭈.  
누군가 먹다 만 거냐쭈? 아니면...  
~ currentSpeaker = Characters.여우
그만 물어! 별거 아니라고 했잖아!
-> afterTalkFox

=== ask_about_mess ===
~ currentSpeaker = Characters.주인공
방이 이렇게 지저분한 이유가 뭐냐쭈? 항상 이런 상태냐쭈?  
~ currentSpeaker = Characters.여우
아... 그냥 요즘 좀 피곤해서! 방 정리할 시간이 없었어. 됐지?  
-> afterTalkFox

=== get_hintCandle ===
~ currentSpeaker = Characters.해설
잘 포장된 선물상자다. 작은 메모에는 'from fox'라고 쓰여 있다.  
메모를 보니 받은 건 아니고 누군가에게 주기 위해 준비한 것 같다.  
선물포장을 뚫고 나오는 향기가 강하다. 향과 관련된 제품인 것 같다.
~ currentSpeaker = Characters.주인공
이건 뭐길래 이렇게 냄새가 강하게 나는 거냐쭈?
~ currentSpeaker = Characters.여우
캔들이라고, 양초인데 태우면 향이 나는 향초야. 그래서 그런 걸껄?
~ currentSpeaker = Characters.주인공
누구에게 줄 거길래 포장까지 예쁘게 하고 쪽지까지 썼냐쭈? 너와는 어울리지 않는다쭈.
~ currentSpeaker = Characters.해설
여우는 얼굴이 빨개지며 빠르게 대답했다.
~ currentSpeaker = Characters.여우
그런 건 상관 없잖아!  
그거야, 그냥 선물이야! 탐정이라고 아무 데나 끼어들지 마!  
~ currentSpeaker = Characters.주인공
(연애 중인가 보군. 알기 쉽다쭈. 좋을 때군.)  
-> afterTalkFox

=== get_hintNeedHelp ===
~ currentSpeaker = Characters.해설
접혀 있는 메모다.  
희미하게 수풀에서 발견한 씨앗에 있던 햄스터의 냄새가 난다.
* 펼쳐보자.
    ~ currentSpeaker = Characters.주인공
    이 메모는 누구 거냐쭈? 네가 쓴 거냐쭈?  
    ~ currentSpeaker = Characters.여우
    뭐? 아니. 내가 쓴 적 없어.  
    이게 뭐지? 아마도... 누군가 여기서 잊고 간 걸 거야.  
    ~ currentSpeaker = Characters.주인공
    (숨기는 걸 잘 못하는 성격 같은데 이번 대답은 명쾌하다쭈. 숨기는 냄새는 아니다쭈.)  
-> choose_home