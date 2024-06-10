Feature: Checkout Process

  Scenario: Complete checkout process
    Given I have selected a product and added it to the cart
    When I proceed to checkout
    And I enter shipping and billing information
     | FirstName   | LastName   | Email   | Country   | City   | Address   | PostalCode   | PhoneNumber   |
     | <FirstName> | <LastName> | <Email> | <Country> | <City> | <Address> | <PostalCode> | <PhoneNumber> |
    Then I should be able to complete the order

    Examples:
    | FirstName | LastName | Email          | Country | City  |  Address   |  PostalCode | PhoneNumber |
    | ahmed     | hassan   | test@test.com  | Egypt   | Cairo |  maadi     |  10001       | 11223344556  |
    | mohamed   | salah    | test0@test.com | Egypt   | Alex  |  maadi     |  11850       | 12345678900  |