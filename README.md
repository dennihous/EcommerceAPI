#### Clone repo

git clone https://github.com/dennihous/EcommerceAPI.git
cd EcommerceAPI

#### Install the NuGet packages

dotnet restore

#### Set up the database

dotnet ef migrations add InitialCreate
dotnet ef database update

#### Run the application

dotnet run --project EcommerceAPI

The API will be available at https://localhost:5001

#### Run unit tests

dotnet test

#### Environement variables

These should already been added from github actions in secrets but if not use this:

export Jwt__Key="YourSuperSecretKeyWithAtLeast32Characters"

### API Endpoints

#### Products
GET /api/products: Get all products.

GET /api/products/{id}: Get a product by ID.

POST /api/products: Create a new product.

PUT /api/products/{id}: Update a product.

DELETE /api/products/{id}: Delete a product.

#### Orders
GET /api/orders: Get all orders.

GET /api/orders/{id}: Get an order by ID.

POST /api/orders: Create a new order.

PUT /api/orders/{id}: Update an order.

DELETE /api/orders/{id}: Delete an order.

#### Customers
GET /api/customers: Get all customers.

GET /api/customers/{id}: Get a customer by ID.

POST /api/customers: Create a new customer.

PUT /api/customers/{id}: Update a customer.

DELETE /api/customers/{id}: Delete a customer.

#### Reviews
GET /api/reviews: Get all reviews.

GET /api/reviews/{id}: Get a review by ID.

POST /api/reviews: Create a new review.

PUT /api/reviews/{id}: Update a review.

DELETE /api/reviews/{id}: Delete a review.