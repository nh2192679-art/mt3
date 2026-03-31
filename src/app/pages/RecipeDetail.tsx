import { useParams, Link } from 'react-router';
import { recipes } from '../data/recipes';
import { Clock, Users, Flame, ArrowLeft, Heart, Share2, Print, CheckCircle2 } from 'lucide-react';
import { Button } from '../components/ui/button';
import { Badge } from '../components/ui/badge';
import { Checkbox } from '../components/ui/checkbox';
import { Separator } from '../components/ui/separator';
import { useState } from 'react';
import { RecipeCard } from '../components/RecipeCard';

export function RecipeDetail() {
  const { id } = useParams();
  const recipe = recipes.find((r) => r.id === Number(id));
  const [checkedIngredients, setCheckedIngredients] = useState<Set<number>>(new Set());
  const [isSaved, setIsSaved] = useState(false);

  if (!recipe) {
    return (
      <div className="container mx-auto px-4 py-20 text-center">
        <h2 className="mb-4">Recipe not found</h2>
        <Link to="/">
          <Button>Back to Home</Button>
        </Link>
      </div>
    );
  }

  const toggleIngredient = (index: number) => {
    const newChecked = new Set(checkedIngredients);
    if (newChecked.has(index)) {
      newChecked.delete(index);
    } else {
      newChecked.add(index);
    }
    setCheckedIngredients(newChecked);
  };

  const relatedRecipes = recipes
    .filter((r) => r.category === recipe.category && r.id !== recipe.id)
    .slice(0, 3);

  return (
    <div className="min-h-screen">
      {/* Back Button */}
      <div className="container mx-auto px-4 py-4">
        <Link to="/">
          <Button variant="ghost" size="sm">
            <ArrowLeft className="h-4 w-4 mr-2" />
            Back to Recipes
          </Button>
        </Link>
      </div>

      {/* Hero Image */}
      <div className="relative h-[400px] lg:h-[500px]">
        <img
          src={recipe.image}
          alt={recipe.title}
          className="w-full h-full object-cover"
        />
        <div className="absolute inset-0 bg-gradient-to-t from-black/60 to-transparent"></div>
        <div className="absolute bottom-0 left-0 right-0 p-6 lg:p-12">
          <div className="container mx-auto">
            <Badge className="mb-4 bg-primary">{recipe.category}</Badge>
            <h1 className="text-3xl lg:text-5xl text-white mb-4">{recipe.title}</h1>
            <div className="flex flex-wrap gap-4 text-white/90">
              <div className="flex items-center space-x-2">
                <Clock className="h-5 w-5" />
                <span>{recipe.cookTime} minutes</span>
              </div>
              <div className="flex items-center space-x-2">
                <Users className="h-5 w-5" />
                <span>{recipe.servings} servings</span>
              </div>
              <div className="flex items-center space-x-2">
                <Flame className="h-5 w-5" />
                <span>{recipe.calories} calories</span>
              </div>
              <div className="flex items-center space-x-2">
                <span className="text-yellow-400">⭐ {recipe.rating}</span>
              </div>
            </div>
          </div>
        </div>
      </div>

      {/* Action Buttons */}
      <div className="bg-white border-b border-border sticky top-16 z-40">
        <div className="container mx-auto px-4 py-4">
          <div className="flex flex-wrap gap-3">
            <Button
              variant={isSaved ? 'default' : 'outline'}
              onClick={() => setIsSaved(!isSaved)}
            >
              <Heart className={`h-4 w-4 mr-2 ${isSaved ? 'fill-current' : ''}`} />
              {isSaved ? 'Saved' : 'Save Recipe'}
            </Button>
            <Button variant="outline">
              <Share2 className="h-4 w-4 mr-2" />
              Share
            </Button>
            <Button variant="outline">
              <Print className="h-4 w-4 mr-2" />
              Print
            </Button>
            <Button variant="default">Add to Meal Plan</Button>
          </div>
        </div>
      </div>

      {/* Content */}
      <div className="container mx-auto px-4 py-12">
        <div className="grid grid-cols-1 lg:grid-cols-3 gap-12">
          {/* Main Content */}
          <div className="lg:col-span-2">
            {/* Description */}
            <div className="mb-8">
              <h2 className="mb-4">Description</h2>
              <p className="text-muted-foreground leading-relaxed">{recipe.description}</p>
              <div className="flex flex-wrap gap-2 mt-4">
                {recipe.tags.map((tag, index) => (
                  <Badge key={index} variant="secondary">
                    {tag}
                  </Badge>
                ))}
              </div>
            </div>

            <Separator className="my-8" />

            {/* Ingredients */}
            <div className="mb-8">
              <h2 className="mb-6">Ingredients</h2>
              <div className="bg-muted/50 rounded-2xl p-6">
                <div className="space-y-4">
                  {recipe.ingredients.map((ingredient, index) => (
                    <div key={index} className="flex items-center space-x-3">
                      <Checkbox
                        id={`ingredient-${index}`}
                        checked={checkedIngredients.has(index)}
                        onCheckedChange={() => toggleIngredient(index)}
                      />
                      <label
                        htmlFor={`ingredient-${index}`}
                        className={`flex-1 cursor-pointer ${
                          checkedIngredients.has(index)
                            ? 'line-through text-muted-foreground'
                            : ''
                        }`}
                      >
                        <span className="font-medium">{ingredient.amount}</span> {ingredient.item}
                      </label>
                    </div>
                  ))}
                </div>
                <Button variant="outline" className="w-full mt-6">
                  Add All to Shopping List
                </Button>
              </div>
            </div>

            <Separator className="my-8" />

            {/* Instructions */}
            <div className="mb-8">
              <h2 className="mb-6">Instructions</h2>
              <div className="space-y-6">
                {recipe.steps.map((step) => (
                  <div key={step.step} className="flex space-x-4">
                    <div className="flex-shrink-0 w-10 h-10 bg-primary text-primary-foreground rounded-full flex items-center justify-center">
                      {step.step}
                    </div>
                    <div className="flex-1 pt-1">
                      <p className="leading-relaxed">{step.instruction}</p>
                    </div>
                  </div>
                ))}
              </div>
            </div>

            {/* Video Section */}
            {recipe.video && (
              <>
                <Separator className="my-8" />
                <div>
                  <h2 className="mb-6">Video Tutorial</h2>
                  <div className="aspect-video bg-muted rounded-2xl flex items-center justify-center">
                    <p className="text-muted-foreground">Video player would go here</p>
                  </div>
                </div>
              </>
            )}

            {/* Comments Section */}
            <Separator className="my-8" />
            <div>
              <h2 className="mb-6">Reviews & Comments</h2>
              <div className="space-y-6">
                {[1, 2, 3].map((comment) => (
                  <div key={comment} className="bg-muted/50 rounded-2xl p-6">
                    <div className="flex items-start space-x-4">
                      <div className="w-10 h-10 bg-primary rounded-full flex items-center justify-center text-primary-foreground">
                        U
                      </div>
                      <div className="flex-1">
                        <div className="flex items-center justify-between mb-2">
                          <h4>User {comment}</h4>
                          <div className="flex items-center">
                            <span className="text-yellow-400">⭐⭐⭐⭐⭐</span>
                          </div>
                        </div>
                        <p className="text-muted-foreground text-sm">
                          This recipe was absolutely delicious! Made it for dinner last night and
                          everyone loved it. Will definitely make again.
                        </p>
                        <p className="text-xs text-muted-foreground mt-2">2 days ago</p>
                      </div>
                    </div>
                  </div>
                ))}
              </div>
            </div>
          </div>

          {/* Sidebar */}
          <div className="lg:col-span-1">
            <div className="sticky top-32">
              {/* Nutrition Info */}
              <div className="bg-card rounded-2xl p-6 border border-border mb-6">
                <h3 className="mb-4">Nutrition Facts</h3>
                <div className="space-y-3">
                  <div className="flex justify-between">
                    <span className="text-muted-foreground">Calories</span>
                    <span>{recipe.calories}</span>
                  </div>
                  <Separator />
                  <div className="flex justify-between">
                    <span className="text-muted-foreground">Protein</span>
                    <span>28g</span>
                  </div>
                  <Separator />
                  <div className="flex justify-between">
                    <span className="text-muted-foreground">Carbs</span>
                    <span>32g</span>
                  </div>
                  <Separator />
                  <div className="flex justify-between">
                    <span className="text-muted-foreground">Fat</span>
                    <span>18g</span>
                  </div>
                </div>
              </div>

              {/* Cook's Notes */}
              <div className="bg-accent/30 rounded-2xl p-6 border border-border mb-6">
                <div className="flex items-start space-x-3">
                  <CheckCircle2 className="h-5 w-5 text-primary flex-shrink-0 mt-0.5" />
                  <div>
                    <h4 className="mb-2">Pro Tip</h4>
                    <p className="text-sm text-muted-foreground">
                      For best results, let the chicken rest for 5 minutes before serving to retain
                      juices.
                    </p>
                  </div>
                </div>
              </div>

              {/* Related Recipes */}
              <div>
                <h3 className="mb-4">Similar Recipes</h3>
                <div className="space-y-4">
                  {relatedRecipes.map((relatedRecipe) => (
                    <Link
                      key={relatedRecipe.id}
                      to={`/recipe/${relatedRecipe.id}`}
                      className="flex space-x-3 bg-card rounded-xl p-3 hover:shadow-md transition-shadow border border-border"
                    >
                      <img
                        src={relatedRecipe.image}
                        alt={relatedRecipe.title}
                        className="w-20 h-20 object-cover rounded-lg"
                      />
                      <div className="flex-1 min-w-0">
                        <h4 className="text-sm line-clamp-2 mb-1">{relatedRecipe.title}</h4>
                        <div className="flex items-center text-xs text-muted-foreground space-x-2">
                          <Clock className="h-3 w-3" />
                          <span>{relatedRecipe.cookTime}min</span>
                          <span>⭐ {relatedRecipe.rating}</span>
                        </div>
                      </div>
                    </Link>
                  ))}
                </div>
              </div>
            </div>
          </div>
        </div>

        {/* More Recipes Section */}
        <Separator className="my-12" />
        <div>
          <h2 className="mb-8">You Might Also Like</h2>
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
            {recipes.slice(0, 4).map((recipe) => (
              <RecipeCard key={recipe.id} recipe={recipe} />
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}
