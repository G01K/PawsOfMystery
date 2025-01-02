=== goto_peacockRoom ===
~ foundPeacockFeather += 1

~ currentSpeaker = Characters.해설
족제비는 다스를 데리고 공작의 집으로 향했다.
~ currentSpeaker = Characters.족제비
공작은 착하고 남들을 잘 챙겨주는 좋은 친구야.  
미적 감각도 좋아서 그림을 그리면 그리는 대로 팔리는 우리 마을의 화가야.  
자, 공작의 집은 여기야.
~ currentSpeaker = Characters.해설
공작의 집은 멀리서 봐도 눈에 띄는 편으로  
정원에는 정교하게 다듬어진 나무들과 아름다운 꽃들이 가득했다.  
족제비가 문을 두드리자 공작이 직접 문을 열었다.
~ currentSpeaker = Characters.공작
어머, 이게 누구야? 족제비와... 귀여운 고양이 아니야?
~ currentSpeaker = Characters.주인공
'귀여운'도 맞고 '고양이'도 맞지만,  
귀여운 고양이 탐정이기도 하다쭈. 탐정 '다쭈'다쭈!
~ currentSpeaker = Characters.공작
탐정? 깜찍하네. 무슨 일이야, 나를 의심하려는 건 아니겠지?
~ currentSpeaker = Characters.주인공
그건 상황에 따라 달려있쭈.  
바로 본론으로 들어가겠쭈.  
어제 저녁에 어디 있었냐쭈?
~ currentSpeaker = Characters.공작
어제 저녁? 아, 나는 혼자 책을 읽고 있었어.  
너희가 나를 의심할 이유는 없을 텐데?

-> afterTalkPeacock

-(afterTalkPeacock)
{ afterTalkPeacock == 1 :
    ~ currentSpeaker = Characters.해설
    다스는 공작과 대화를 나누며 방을 조사하기 시작했다.
    ~ currentSpeaker = Characters.해설
    냄새를 맡아보자
}
{ StartButtonAnimation() }

* [공작의 책상 위에서 참새와의 대화 기록을 조사한다.]  
    -> get_hintMassageToSparrow
* [공작의 방 한쪽에 놓인 쇼핑 리스트 메모를 조사한다.]
    -> get_hintShoppingListMemo
* [공작의 방 한쪽에 놓인 화려한 거울을 조사한다.]
    -> get_hintMyself
+ [돌아간다.]
    -> choose_home

= get_hintMassageToSparrow
    ~ add_item(massageToSparrow)

~ currentSpeaker = Characters.해설
책상 위에 놓인 스마트폰 화면에  
'참새'와의 대화가 있었다.  

공작: "거봐 내가 저번에 말했잖아 웃기는 애라니까? 아까 볼에 가득 음식을 구겨 넣는 거 봤지?"  
참새: "ㅋㅋ 진짜 웃겨. >< 근데 걔는 왜 그렇게 예민한 거야?"  
공작: "그냥 원래 그런 애니까. 자기가 다 맞는 줄 아는 거지."  

~ currentSpeaker = Characters.해설
더 보려는 순간, 공작이 핸드폰을 낚아채며 말을 끊었다.  
~ currentSpeaker = Characters.공작
잠깐! 왜 남의 핸드폰을 멋대로 보는 거야?  

* [시치미를 때고 못 본 척한다.  ]
    -> ignore_phone
* [방금 본 내용에 대해 캐묻는다.  ]
    -> confront_peacock_about_phone

= ignore_phone
~ currentSpeaker = Characters.주인공
이건! 이번에 새로 나온 '우주 바나나 은하 회사'의 한정판 시리즈 핸드폰인 '반짝반짝폰' 아니냐쭈?  
아직 정식 발매도 안 되었는데 어떻게 얻은 거냐쭈?  
나도 알려주라쭈!
~ currentSpeaker = Characters.해설
공작은 보기 드물게 당황했다가, 이내 여유를 되찾으며 묘한 미소를 지었다.
~ currentSpeaker = Characters.공작
발매는 이미 되었어. 물론 수량이 한정적이라 구하기 엄청 힘들었지만,  
나는 특별한 인맥이 있거든. 겨우 하나 손에 넣었어. 물론 나니까 가능한 일이야.
~ currentSpeaker = Characters.주인공
나도 그 친구를 소개시켜주라쭈!
~ currentSpeaker = Characters.공작
흠, 이번 시리즈는 이미 다 팔렸으니 그 친구도 못 구할 거야.  
하지만 다음에 새로운 시리즈가 나온다면 미리 알려줄게.  
물론... 내가 기분이 좋다면 말이야.  
~ currentSpeaker = Characters.주인공
(메시지를 본 건 공작이 눈치 못 챈 것 같다쭈. 캐물으면 안 될 것 같다쭈.)  

-> afterTalkPeacock

= confront_peacock_about_phone
~ currentSpeaker = Characters.주인공
공작, 아까 참새랑 나눈 대화가 좀 이상하던데쭈.  
'웃기는 애라니까'라니... 햄스터를 두고 한 말이냐쭈?
~ currentSpeaker = Characters.공작
뭐? 갑자기 무슨 소리야?  
웃기지도 않네. 너 정말 엉뚱한 상상력이 풍부하구나.
~ currentSpeaker = Characters.해설
공작은 다스와 족제비를 방에서 내쫓아버렸다.  
공작의 방에서 추가 증거를 더 이상 수집할 수 없게 되었다.
~ currentSpeaker = Characters.주인공
(이 방에서 더 이상 단서를 찾을 수 없게 된 건 아쉽다쭈...)

-> choose_home

= get_hintShoppingListMemo
    ~ add_item(shoppingListMemo)
~ currentSpeaker = Characters.해설
탁자 위에는 쇼핑 리스트가 적힌 메모가 있다.  
"향수, 깃털브러시, 깃털염색약"이라는 항목이 적혀 있다.  
글씨체는 여우 방에서 발견한 메모와 동일해 보인다.
~ currentSpeaker = Characters.주인공
이 메모는 뭐냐쭈? 네가 쓴 거냐쭈?
~ currentSpeaker = Characters.공작
그건 내가 적은 쇼핑 리스트야. 무슨 문제라도 있어?
~ currentSpeaker = Characters.주인공
여우의 방에서 너가 적은 쪽지를 본 적이 있다쭈.  
이 메모와 같은 글씨체로 '로맨틱한 향초 캔들 선물이 좋겠어'라고 써 있었쭈.  
여우는 자기가 쓴 거라던데 그게 무슨 뜻이냐쭈?
~ currentSpeaker = Characters.공작
아~ 그건... 여우가 좋아하는 친구에게 줄 선물을 고르는데  
무슨 선물을 줘야 할지 모르겠다고 하길래 내가 추천해준 거야.  
누군가를 감동시키는 법은 내가 더 잘 알거든.
~ currentSpeaker = Characters.주인공
그런데 왜 여우는 너가 쓴 쪽지라는 걸 숨긴 거냐쭈?
~ currentSpeaker = Characters.공작
그야 당연히... 여우는 창피했던 거야. 너는 그런 것도 몰라주는구나?  
역시 여우를 진심으로 생각해주는 건 나밖에 없는 것 같네.
-> afterTalkPeacock

= get_hintMyself
    ~ add_item(myself)

~ currentSpeaker = Characters.해설
방 한쪽에 놓인 거울은 지나치게 화려했다.  
거울 앞에는 공작의 초상화가 놓여 있었고,  
"세상에서 가장 멋진 나"라고 적힌 글귀가 새겨져 있었다.
~ currentSpeaker = Characters.주인공
대단하군쭈... 넌 네가 세상에서 제일 멋지다고 생각하냐쭈?  
~ currentSpeaker = Characters.공작
그럼, 왜 아니겠어?  
자기애는 건강한 거라고 생각하지 않아?
-> afterTalkPeacock