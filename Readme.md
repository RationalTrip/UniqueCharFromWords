# Завдання

Вам потрібно розробити алгоритм програми, яка повинна виконувати наступне:
- програма приймає на вхід довільний текст і знаходить в кожному слові цього тексту найперший символ, який більше НЕ повторюється в аналізуємому слові
- далі із отриманого набору символів програма повинна вибрати перший унікальний (тобто той, який більше не зустручається в наборі) і повернути його.

Наприклад, якщо програма отримує на вхід текст нижче:

The Tao gave birth to machine language.  Machine language gave birth
to the assembler.
The assembler gave birth to the compiler.  Now there are ten thousand
languages.
Each language has its purpose, however humble.  Each language
expresses the Yin and Yang of software.  Each language has its place within
the Tao.
But do not program in COBOL if you can avoid it.
        -- Geoffrey James, "The Tao of Programming"

то повинна повернути вона символ "m".

# Реалізація

Реалізація завдання написана мовою C# і .Net 6.

Для запуску запустіть програму використовуючи Visual Studio, або інші інструменти.

Також в папці tests написані базові Unit тести (використовуючи xUnit), для базової перевірки роботи модулів алгоритму.

# Вхідні параметри

Вхідні параметри передаються в консолі, кінцем вводу вважається введення путої строки.

# Особливості реалізації

Логіка  роботи розбита на 3 частини, що розділені по своїх типах:

- Колекція перебору слів у строці. Звичайно для даних цілей можна було використовувати метод Split, однак для великих строк з великою кількістю слів, виділяється надто багато пам'яті, і працює повільно. Тому було вирішено написати власний перебір, що повертає ReadOnlySpan, що не виділяє додаткової пам'яті `src\TextFirstUniqueCharSelector\Logic\Collections\WordEnumerator.cs`

- Колекція, що відслідковує унікальність та порядок додавання елеметнів. Для цього використовуються 2 колекції. LinkedList - використовується для відслідкування порядку доданих елементів, була вибрана дана колекція, оскільки вона дозволяє швидко видаляти елементи при винекненні дублів. Dictionary - що відслідковує унікальність елементів (використовується, оскільки час перевірки унікальності O(1)), також у ньому зберігаються вузли LinkedList, що видаляються при виявленні дублів `src\TextFirstUniqueCharSelector\Logic\Collections\UniqueOrderedValues.cs`

- Клас з основною логікою, що приймає текст, та зберігає відповідно використовуючи 2 попередні класи виконує основу логіку. `src\TextFirstUniqueCharSelector\Logic\CharFromTextSelector.cs`

# Основні ідеї реалізації

1. За слова розглядаються послідовність букв, для визанчення букви використовується метод `WordEnumerator.IsLetter`, за необхідності можна змінити. (для прикладу апостроф не вважається словом, вважається роздільником). Те саме стосується реалізації переносу слова на новий рядок.
2. Вважається, що слово є невеликим, тому для пошуку унікального символу в ньому буде ефективнішим перебрати слово, перевіривши чи є збіги у ньому.