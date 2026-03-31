import { Clock, Star, Heart, ShoppingCart } from 'lucide-react';
import { Link } from 'react-router';
import { Recipe } from '../data/recipes';
import { Button } from './ui/button';
import { Badge } from './ui/badge';
import { useState } from 'react';

interface RecipeCardProps {
  recipe: Recipe;
}

export function RecipeCard({ recipe }: RecipeCardProps) {
  const [isSaved, setIsSaved] = useState(false);

  return (
    <div className="group bg-card rounded-2xl overflow-hidden shadow-md hover:shadow-xl transition-all duration-300 hover:-translate-y-1">
      <Link to={`/recipe/${recipe.id}`} className="block relative overflow-hidden">
        <img
          src={recipe.image}
          alt={recipe.title}
          className="w-full h-48 object-cover transition-transform duration-300 group-hover:scale-110"
          loading="lazy"
        />
        <div className="absolute top-3 right-3 flex space-x-2">
          {recipe.isTrending && (
            <Badge className="bg-secondary text-secondary-foreground">🔥 Trending</Badge>
          )}
        </div>
      </Link>

      <div className="p-5">
        <Link to={`/recipe/${recipe.id}`}>
          <div className="flex items-start justify-between mb-2">
            <Badge variant="outline" className="text-xs">
              {recipe.category}
            </Badge>
            <div className="flex items-center space-x-1 text-sm">
              <Star className="h-4 w-4 fill-yellow-400 text-yellow-400" />
              <span>{recipe.rating}</span>
            </div>
          </div>

          <h3 className="mb-2 line-clamp-2 group-hover:text-primary transition-colors">
            {recipe.title}
          </h3>

          <p className="text-muted-foreground text-sm mb-4 line-clamp-2">
            {recipe.description}
          </p>

          <div className="flex items-center justify-between text-sm text-muted-foreground">
            <div className="flex items-center space-x-1">
              <Clock className="h-4 w-4" />
              <span>{recipe.cookTime} min</span>
            </div>
            <div className="text-xs">{recipe.calories} cal</div>
          </div>
        </Link>

        <div className="flex space-x-2 mt-4 pt-4 border-t border-border">
          <Button
            variant={isSaved ? 'default' : 'outline'}
            size="sm"
            className="flex-1"
            onClick={() => setIsSaved(!isSaved)}
          >
            <Heart className={`h-4 w-4 mr-1 ${isSaved ? 'fill-current' : ''}`} />
            Save
          </Button>
          <Button variant="outline" size="sm" className="flex-1">
            <ShoppingCart className="h-4 w-4 mr-1" />
            Add
          </Button>
        </div>
      </div>
    </div>
  );
}
