Feature: Biberk Links Verification

Scenario: Verify employer posting notices page for each state
    Given I am on the Biberk website
    When I select the state of <state>
    Then I should be taken to the <state> employer posting notices page
    And I should see the server message "<state>"
    And I should check the dropdown link for each option

Scenario: Verify error message for page not found
    Given I am on the Biberk website
    When I select the state of <state>
    Then I should see an error message that the page cannot be found

Scenario Outline: Verify dropdown link for each option
    Given I am on the Biberk website
    When I select the state of <state>
    Then I should check the dropdown link for "<option>"

Examples:
| state     | option       |
| Alaska    | Option 1     |
| New York  | Option 2     |
| Texas     | Option 3     |
| Delaware  | Option 4     |
| Kentucky  | Option 5     |
| Nebraska  | Option 6     |
| Nevada    | Option 7     |
| New Hampshire | Option 8 |
| New Jersey    | Option 9 |
| Carolina      | Option 10|
| New Mexico    | Option 11|
| South Dakota  | Option 12|
