import { DndProvider, useDrag, useDrop } from 'react-dnd';
import { HTML5Backend } from 'react-dnd-html5-backend';
import { Calendar, ChevronLeft, ChevronRight, Plus, X } from 'lucide-react';
import { useState } from 'react';
import { recipes, Recipe } from '../data/recipes';
import { Button } from '../components/ui/button';
import { Badge } from '../components/ui/badge';

interface MealSlot {
  day: string;
  meal: 'breakfast' | 'lunch' | 'dinner';
  recipe?: Recipe;
}

const DAYS = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'];
const MEALS = ['breakfast', 'lunch', 'dinner'] as const;

function RecipeDragItem({ recipe }: { recipe: Recipe }) {
  const [{ isDragging }, drag] = useDrag(() => ({
    type: 'recipe',
    item: { recipe },
    collect: (monitor) => ({
      isDragging: monitor.isDragging(),
    }),
  }));

  return (
    <div
      ref={drag}
      className={`bg-card rounded-lg p-3 cursor-move hover:shadow-md transition-shadow border border-border ${
        isDragging ? 'opacity-50' : ''
      }`}
    >
      <div className="flex items-center space-x-3">
        <img
          src={recipe.image}
          alt={recipe.title}
          className="w-12 h-12 object-cover rounded-lg"
        />
        <div className="flex-1 min-w-0">
          <h4 className="text-sm line-clamp-1">{recipe.title}</h4>
          <p className="text-xs text-muted-foreground">{recipe.cookTime} min</p>
        </div>
      </div>
    </div>
  );
}

function MealDropZone({
  day,
  meal,
  recipe,
  onDrop,
  onRemove,
}: {
  day: string;
  meal: string;
  recipe?: Recipe;
  onDrop: (recipe: Recipe) => void;
  onRemove: () => void;
}) {
  const [{ isOver }, drop] = useDrop(() => ({
    accept: 'recipe',
    drop: (item: { recipe: Recipe }) => onDrop(item.recipe),
    collect: (monitor) => ({
      isOver: monitor.isOver(),
    }),
  }));

  return (
    <div
      ref={drop}
      className={`min-h-[100px] rounded-xl border-2 border-dashed p-3 transition-colors ${
        isOver
          ? 'border-primary bg-primary/5'
          : recipe
          ? 'border-border bg-card'
          : 'border-border bg-muted/30'
      }`}
    >
      {recipe ? (
        <div className="relative group">
          <button
            onClick={onRemove}
            className="absolute -top-2 -right-2 w-6 h-6 bg-destructive text-destructive-foreground rounded-full flex items-center justify-center opacity-0 group-hover:opacity-100 transition-opacity"
          >
            <X className="h-4 w-4" />
          </button>
          <img
            src={recipe.image}
            alt={recipe.title}
            className="w-full h-20 object-cover rounded-lg mb-2"
          />
          <h4 className="text-sm line-clamp-2 mb-1">{recipe.title}</h4>
          <div className="flex items-center justify-between text-xs text-muted-foreground">
            <span>{recipe.cookTime} min</span>
            <span>{recipe.calories} cal</span>
          </div>
        </div>
      ) : (
        <div className="flex flex-col items-center justify-center h-full text-muted-foreground">
          <Plus className="h-6 w-6 mb-1" />
          <p className="text-xs">Drop recipe here</p>
        </div>
      )}
    </div>
  );
}

function MealPlannerContent() {
  const [weekOffset, setWeekOffset] = useState(0);
  const [mealPlan, setMealPlan] = useState<Map<string, Recipe>>(new Map());

  const getMealKey = (day: string, meal: string) => `${day}-${meal}`;

  const handleDrop = (day: string, meal: string, recipe: Recipe) => {
    const newPlan = new Map(mealPlan);
    newPlan.set(getMealKey(day, meal), recipe);
    setMealPlan(newPlan);
  };

  const handleRemove = (day: string, meal: string) => {
    const newPlan = new Map(mealPlan);
    newPlan.delete(getMealKey(day, meal));
    setMealPlan(newPlan);
  };

  const getWeekDateRange = () => {
    const today = new Date();
    const startDate = new Date(today);
    startDate.setDate(today.getDate() + weekOffset * 7);
    const endDate = new Date(startDate);
    endDate.setDate(startDate.getDate() + 6);

    const formatDate = (date: Date) => {
      return date.toLocaleDateString('en-US', { month: 'short', day: 'numeric' });
    };

    return `${formatDate(startDate)} - ${formatDate(endDate)}`;
  };

  return (
    <div className="min-h-screen bg-background py-8">
      <div className="container mx-auto px-4">
        {/* Header */}
        <div className="mb-8">
          <div className="flex items-center justify-between mb-4">
            <div>
              <h1 className="flex items-center">
                <Calendar className="h-8 w-8 mr-3 text-primary" />
                Weekly Meal Planner
              </h1>
              <p className="text-muted-foreground mt-2">
                Drag and drop recipes to plan your meals for the week
              </p>
            </div>
            <div className="flex items-center space-x-2">
              <Button variant="outline" size="icon" onClick={() => setWeekOffset(weekOffset - 1)}>
                <ChevronLeft className="h-4 w-4" />
              </Button>
              <div className="px-4 py-2 bg-muted rounded-lg min-w-[200px] text-center">
                <p className="text-sm font-medium">{getWeekDateRange()}</p>
              </div>
              <Button variant="outline" size="icon" onClick={() => setWeekOffset(weekOffset + 1)}>
                <ChevronRight className="h-4 w-4" />
              </Button>
            </div>
          </div>

          <div className="flex flex-wrap gap-2">
            <Button>Save Plan</Button>
            <Button variant="outline">Generate Shopping List</Button>
            <Button variant="outline">Clear All</Button>
          </div>
        </div>

        <div className="grid grid-cols-1 lg:grid-cols-4 gap-8">
          {/* Recipe Library */}
          <div className="lg:col-span-1">
            <div className="sticky top-20">
              <div className="bg-card rounded-2xl p-6 border border-border">
                <h3 className="mb-4">Recipe Library</h3>
                <p className="text-sm text-muted-foreground mb-4">
                  Drag recipes to your meal plan
                </p>
                <div className="space-y-3 max-h-[600px] overflow-y-auto">
                  {recipes.slice(0, 10).map((recipe) => (
                    <RecipeDragItem key={recipe.id} recipe={recipe} />
                  ))}
                </div>
              </div>
            </div>
          </div>

          {/* Calendar Grid */}
          <div className="lg:col-span-3">
            <div className="bg-card rounded-2xl border border-border overflow-hidden">
              <div className="overflow-x-auto">
                <table className="w-full">
                  <thead>
                    <tr className="border-b border-border bg-muted/50">
                      <th className="p-4 text-left min-w-[100px]">Meal</th>
                      {DAYS.map((day) => (
                        <th key={day} className="p-4 text-left min-w-[180px]">
                          <div className="font-semibold">{day}</div>
                          <div className="text-xs text-muted-foreground font-normal">
                            Mar {31 + DAYS.indexOf(day)}
                          </div>
                        </th>
                      ))}
                    </tr>
                  </thead>
                  <tbody>
                    {MEALS.map((meal) => (
                      <tr key={meal} className="border-b border-border">
                        <td className="p-4 align-top bg-muted/30">
                          <div className="font-medium capitalize">{meal}</div>
                        </td>
                        {DAYS.map((day) => (
                          <td key={day} className="p-4 align-top">
                            <MealDropZone
                              day={day}
                              meal={meal}
                              recipe={mealPlan.get(getMealKey(day, meal))}
                              onDrop={(recipe) => handleDrop(day, meal, recipe)}
                              onRemove={() => handleRemove(day, meal)}
                            />
                          </td>
                        ))}
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            </div>

            {/* Weekly Summary */}
            <div className="mt-8 grid grid-cols-1 md:grid-cols-3 gap-6">
              <div className="bg-card rounded-xl p-6 border border-border">
                <h4 className="mb-2">Total Meals</h4>
                <p className="text-3xl font-semibold text-primary">{mealPlan.size}</p>
                <p className="text-sm text-muted-foreground">out of 21 planned</p>
              </div>
              <div className="bg-card rounded-xl p-6 border border-border">
                <h4 className="mb-2">Avg Calories/Day</h4>
                <p className="text-3xl font-semibold text-primary">
                  {mealPlan.size > 0
                    ? Math.round(
                        Array.from(mealPlan.values()).reduce(
                          (sum, recipe) => sum + recipe.calories,
                          0
                        ) / 7
                      )
                    : 0}
                </p>
                <p className="text-sm text-muted-foreground">per day</p>
              </div>
              <div className="bg-card rounded-xl p-6 border border-border">
                <h4 className="mb-2">Prep Time</h4>
                <p className="text-3xl font-semibold text-primary">
                  {Array.from(mealPlan.values()).reduce(
                    (sum, recipe) => sum + recipe.cookTime,
                    0
                  )}{' '}
                  min
                </p>
                <p className="text-sm text-muted-foreground">total this week</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export function MealPlanner() {
  return (
    <DndProvider backend={HTML5Backend}>
      <MealPlannerContent />
    </DndProvider>
  );
}
