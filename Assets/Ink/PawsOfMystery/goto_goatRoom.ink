=== goto_goatRoom ===
~ foundGoatFootprint += 1

~ currentSpeaker = Characters.해설
염소의 방은 마을에서도 동떨어진 곳에 있었다.  
조용하고 운치 있는 집이었다.  
족제비가 문을 두드려 염소를 불렀다.
~ currentSpeaker = Characters.염소
어서 오게나. 모르는 친구도 한 마리 있군.
~ currentSpeaker = Characters.주인공
내 이름은 다쭈! 고양이 탐정이다쭈!  
햄스터의 실종 사건 조사를 위해 족제비가 의뢰해서 왔다쭈.
~ currentSpeaker = Characters.염소
흠, 그런가? 어떤 조사를 하러 왔는가?
~ currentSpeaker = Characters.해설
염소는 다스가 탐정이라는 사실에도,  
햄스터가 실종되었다는 말에도 별로 놀라지 않은 것 같았다.
~ currentSpeaker = Characters.주인공
햄스터가 어디에 있는지 마음 짚이는 곳이 있냐쭈?
~ currentSpeaker = Characters.염소
... 내가 만약 알려준다고 하면 너는 나를 다 믿을 수 있는가?
~ currentSpeaker = Characters.주인공
무슨 소리냐쭈?
~ currentSpeaker = Characters.염소
말 그대로의 뜻이라네. 남이 해주는 말을 곧이곧대로 듣다가는 아무 답도 얻을 수 없어.  
귓등에서 흘러가는 잔소리와 다를 바가 없지.  
답을 알려면 스스로 알아내야 한다네.
~ currentSpeaker = Characters.해설
염소는 말이 끝나고 뒤로 걸으며 길을 터주었다.  
집에 들어와도 좋다는 표현 같았다.

-> afterTalkGoat

-(afterTalkGoat)
{ afterTalkGoat == 1 :
    ~ currentSpeaker = Characters.해설
    냄새를 맡아보자
}
{ StartButtonAnimation() }

* [찻잔을 조사한다.]
    -> get_hintBrokenPottery
* [동양식 다과상을 조사한다.  ]
    -> get_hintTeaCup
* [화로를 조사한다.  ]
    -> get_hintBurntDiary
* [낡은 책을 조사한다.  ]
    -> get_hintBookWithBookmark
+ [돌아간다.  ]
    -> choose_home

= get_hintBrokenPottery
~ currentSpeaker = Characters.해설
오래되었지만 잘 보존된 서양식 찻잔 세트가 있다.  
정확히는 잘 보존되었던 것 같지만,  
두 개의 찻잔 중 하나는 깨졌다가 다시 복구한 흔적이 있다.  
그 모습이 염소의 집의 고요한 분위기와 대조되어 눈길을 끌었다.  
~ currentSpeaker = Characters.주인공
저 찻잔은 하나가 깨져 있쭈.
~ currentSpeaker = Characters.해설
다스의 말에 족제비가 화들짝 놀라며 다스에게 귓속말을 했다.
~ currentSpeaker = Characters.족제비
다쭈... 저 찻잔은 염소의 옛 부인이 아끼던 거야.  
돌아가신 후로 염소가 그 찻잔을 아끼며 보관했는데,  
햄스터가 실수로 깨뜨렸었어...
~ currentSpeaker = Characters.주인공
(햄스터가 깨뜨렸다니... 이게 원한을 살 만한 이유가 될 수 있쭈?)  
~ currentSpeaker = Characters.염소
찻잔은 의미 없는 껍데기일 뿐이네.  
모든 것은 연이 닿아 잠시 만나는 것인데, 내가 미련을 두는 것뿐이지.  
나는 괜찮으니 편하게 이야기하게나.
-> afterTalkGoat

= get_hintTeaCup
~ currentSpeaker = Characters.해설
동양식 다과상이 가지런히 놓여 있었다.  
두 개의 찻잔과 미처 치우지 못한 차의 흔적이 보인다.  
이곳에서 혼자만의 다과를 즐긴 것은 아닌 듯했다.
~ currentSpeaker = Characters.주인공
차를 마실 때 누구와 함께였냐쭈?
~ currentSpeaker = Characters.염소
햄스터와 마셨다네. 그가 여기서 나와 함께 차를 마시며 많은 이야기를 나누었지.  
평소보다 오래 머물렀던 것도 기억이 나는군.
-> afterTalkGoat

= get_hintBurntDiary
~ currentSpeaker = Characters.해설
동양식 다과상 옆 방 한쪽에 작은 화로가 놓여 있었다.  
그 안에는 희미하게 타다 남은 재가 보인다.  
재에서 나는 냄새는 종이를 태운 흔적과 같았다.
~ currentSpeaker = Characters.주인공
이건... 뭔가쭈? 무언가를 태운 것 같은데?
~ currentSpeaker = Characters.족제비
다쭈, 저건 염소가 일기를 태우는 화로야.  
염소는 중요한 내용을 적은 뒤, 모두 태워버리는 거지.  
염소는 늘 이런 식으로 자신의 생각을 정리하곤 해.
-> afterTalkGoat

= get_hintBookWithBookmark
~ currentSpeaker = Characters.해설
손이 많이 탄 것 같은 낡은 책에 책갈피가 꽂혀 있었다.
* [책갈피가 꽂힌 페이지를 열어본다.  ]
    -> read_bookmark_page

= read_bookmark_page
~ currentSpeaker = Characters.해설
“타인의 깨달음은 누구도 대신할 수 없다.  
스스로 비추어 보고, 스스로 깨닫지 않으면 그 앎은 헛되다.”
~ currentSpeaker = Characters.주인공
염소가 아까 했던 말이랑 비슷하다쭈.  
뭘 알고 있긴 한 것 같은데... 이래선 말해줄 것 같지 않다쭈.
-> afterTalkGoat