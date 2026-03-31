import { Search, TrendingUp, ChefHat, Clock, Flame } from 'lucide-react';
import { useState } from 'react';
import { Link } from 'react-router';
import { recipes, categories, cookingTips } from '../data/recipes';
import { RecipeCard } from '../components/RecipeCard';
import { Button } from '../components/ui/button';
import { Input } from '../components/ui/input';
import { Badge } from '../components/ui/badge';

export function Home() {
  const [aiSearch, setAiSearch] = useState('');

  const featuredRecipes = recipes.filter((r) => r.isFeatured);
  const trendingRecipes = recipes.filter((r) => r.isTrending);

  const aiSuggestions = [
    'Healthy dinner under 30 minutes',
    'Vegetarian pasta recipes',
    'Easy desserts for beginners',
    'High protein breakfast ideas',
  ];

  return (
    <div className="min-h-screen">
      {/* Hero Section */}
      <section className="relative bg-gradient-to-br from-primary/10 via-secondary/10 to-accent/10 py-16 lg:py-24 overflow-hidden">
        <div className="absolute inset-0 opacity-10">
          <div className="absolute top-10 left-10 w-20 h-20 bg-primary rounded-full blur-3xl"></div>
          <div className="absolute bottom-20 right-20 w-32 h-32 bg-secondary rounded-full blur-3xl"></div>
        </div>

        <div className="container mx-auto px-4 relative z-10">
          <div className="max-w-3xl mx-auto text-center">
            <Badge className="mb-4 bg-primary/20 text-primary border-primary/30">
              <Flame className="h-3 w-3 mr-1" />
              AI-Powered Recipe Discovery
            </Badge>
            <h1 className="text-4xl lg:text-6xl mb-6 text-foreground">
              What do you want to cook today?
            </h1>
            <p className="text-lg text-muted-foreground mb-8">
              Discover thousands of delicious recipes with AI assistance
            </p>

            {/* AI Search Input */}
            <div className="relative max-w-2xl mx-auto mb-6">
              <div className="absolute left-4 top-1/2 transform -translate-y-1/2">
                <ChefHat className="h-5 w-5 text-primary" />
              </div>
              <Input
                type="text"
                placeholder="Ask AI: 'What can I make with chicken and broccoli?'"
                value={aiSearch}
                onChange={(e) => setAiSearch(e.target.value)}
                className="pl-12 pr-4 h-14 text-base bg-white border-2 border-primary/20 focus:border-primary rounded-full shadow-lg"
              />
              <Button
                size="lg"
                className="absolute right-2 top-1/2 transform -translate-y-1/2 rounded-full"
              >
                <Search className="h-5 w-5" />
              </Button>
            </div>

            {/* AI Suggestions */}
            <div className="flex flex-wrap justify-center gap-2">
              {aiSuggestions.map((suggestion, index) => (
                <button
                  key={index}
                  onClick={() => setAiSearch(suggestion)}
                  className="px-4 py-2 bg-white hover:bg-primary/10 rounded-full text-sm transition-colors border border-border"
                >
                  {suggestion}
                </button>
              ))}
            </div>
          </div>
        </div>
      </section>

      {/* Categories Section */}
      <section className="py-12 container mx-auto px-4">
        <div className="flex items-center justify-between mb-8">
          <h2>Browse by Category</h2>
          <Link to="/categories">
            <Button variant="ghost">View All →</Button>
          </Link>
        </div>
        <div className="grid grid-cols-2 md:grid-cols-4 lg:grid-cols-8 gap-4">
          {categories.map((category) => (
            <Link
              key={category.id}
              to={`/categories`}
              className="group bg-card rounded-2xl p-4 text-center hover:shadow-lg transition-all hover:-translate-y-1 border border-border"
            >
              <div className="text-4xl mb-2">{category.icon}</div>
              <h4 className="text-sm mb-1">{category.name}</h4>
              <p className="text-xs text-muted-foreground">{category.count} recipes</p>
            </Link>
          ))}
        </div>
      </section>

      {/* Featured Recipes Section */}
      <section className="py-12 bg-muted/30">
        <div className="container mx-auto px-4">
          <div className="flex items-center justify-between mb-8">
            <div>
              <h2 className="flex items-center">
                <TrendingUp className="h-6 w-6 mr-2 text-primary" />
                Featured Recipes
              </h2>
              <p className="text-muted-foreground mt-1">
                Handpicked by our chefs for you
              </p>
            </div>
            <Link to="/recipes">
              <Button variant="outline">View All</Button>
            </Link>
          </div>
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
            {featuredRecipes.slice(0, 4).map((recipe) => (
              <RecipeCard key={recipe.id} recipe={recipe} />
            ))}
          </div>
        </div>
      </section>

      {/* Trending Section - Horizontal Scroll */}
      <section className="py-12 container mx-auto px-4">
        <div className="flex items-center justify-between mb-8">
          <div>
            <h2 className="flex items-center">
              <Flame className="h-6 w-6 mr-2 text-primary" />
              Trending This Week
            </h2>
            <p className="text-muted-foreground mt-1">
              Most popular recipes right now
            </p>
          </div>
        </div>
        <div className="flex overflow-x-auto gap-6 pb-4 scrollbar-hide">
          {trendingRecipes.map((recipe) => (
            <div key={recipe.id} className="flex-shrink-0 w-72">
              <RecipeCard recipe={recipe} />
            </div>
          ))}
        </div>
      </section>

      {/* Cooking Tips Sidebar-style Section */}
      <section className="py-12 bg-gradient-to-br from-accent/20 to-secondary/20">
        <div className="container mx-auto px-4">
          <div className="max-w-4xl mx-auto">
            <h2 className="text-center mb-8">Quick Cooking Tips</h2>
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
              {cookingTips.map((tip) => (
                <div
                  key={tip.id}
                  className="bg-white rounded-2xl p-6 shadow-md hover:shadow-lg transition-shadow border border-border"
                >
                  <div className="flex items-start space-x-3">
                    <div className="w-10 h-10 bg-primary/10 rounded-full flex items-center justify-center flex-shrink-0">
                      <span className="text-xl">💡</span>
                    </div>
                    <div>
                      <h4 className="mb-2">{tip.title}</h4>
                      <p className="text-muted-foreground text-sm">{tip.tip}</p>
                    </div>
                  </div>
                </div>
              ))}
            </div>
            <div className="text-center mt-8">
              <Link to="/tips">
                <Button size="lg">View All Tips</Button>
              </Link>
            </div>
          </div>
        </div>
      </section>

      {/* CTA Section */}
      <section className="py-16 container mx-auto px-4">
        <div className="bg-gradient-to-r from-primary to-secondary rounded-3xl p-8 lg:p-12 text-center text-white">
          <Clock className="h-12 w-12 mx-auto mb-4" />
          <h2 className="text-white mb-4">Start Planning Your Weekly Meals</h2>
          <p className="text-white/90 mb-6 max-w-2xl mx-auto">
            Save time and eat better with our smart meal planner. Drag and drop recipes into your
            weekly calendar.
          </p>
          <Link to="/meal-planner">
            <Button size="lg" variant="secondary" className="shadow-lg">
              Try Meal Planner
            </Button>
          </Link>
        </div>
      </section>
    </div>
  );
}
