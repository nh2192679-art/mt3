import { cookingTips } from '../data/recipes';
import { Lightbulb, ChefHat, Clock, Flame } from 'lucide-react';

const tipsCategories = [
  {
    icon: ChefHat,
    title: 'Cooking Techniques',
    color: 'text-primary',
    bgColor: 'bg-primary/10',
  },
  {
    icon: Clock,
    title: 'Time Savers',
    color: 'text-secondary',
    bgColor: 'bg-secondary/10',
  },
  {
    icon: Flame,
    title: 'Flavor Boosters',
    color: 'text-destructive',
    bgColor: 'bg-destructive/10',
  },
];

const allTips = [
  ...cookingTips,
  {
    id: 5,
    title: 'Room Temperature Ingredients',
    tip: 'Let eggs and butter come to room temperature for better baking results.',
  },
  {
    id: 6,
    title: 'Taste As You Go',
    tip: 'Always taste your food while cooking and adjust seasonings accordingly.',
  },
  {
    id: 7,
    title: 'Hot Pan, Cold Oil',
    tip: 'Heat your pan first, then add oil to prevent sticking.',
  },
  {
    id: 8,
    title: 'Mise en Place',
    tip: 'Prep all ingredients before you start cooking for a smoother process.',
  },
  {
    id: 9,
    title: 'Season in Layers',
    tip: 'Add salt and seasoning throughout cooking, not just at the end.',
  },
  {
    id: 10,
    title: 'Rest Your Meat',
    tip: 'Let meat rest after cooking to redistribute juices for maximum tenderness.',
  },
];

export function Tips() {
  return (
    <div className="min-h-screen">
      <div className="bg-gradient-to-br from-primary/10 to-secondary/10 py-12">
        <div className="container mx-auto px-4">
          <div className="flex items-center space-x-3 mb-4">
            <Lightbulb className="h-10 w-10 text-primary" />
            <h1>Cooking Tips & Tricks</h1>
          </div>
          <p className="text-muted-foreground">
            Master the kitchen with expert advice from professional chefs
          </p>
        </div>
      </div>

      <div className="container mx-auto px-4 py-12">
        {/* Categories */}
        <div className="grid grid-cols-1 md:grid-cols-3 gap-6 mb-12">
          {tipsCategories.map((category, index) => (
            <div
              key={index}
              className="bg-card rounded-2xl p-6 border border-border hover:shadow-lg transition-shadow"
            >
              <div className={`w-12 h-12 ${category.bgColor} rounded-xl flex items-center justify-center mb-4`}>
                <category.icon className={`h-6 w-6 ${category.color}`} />
              </div>
              <h3 className="mb-2">{category.title}</h3>
              <p className="text-muted-foreground text-sm">
                Learn essential {category.title.toLowerCase()} to elevate your cooking
              </p>
            </div>
          ))}
        </div>

        {/* All Tips */}
        <div>
          <h2 className="mb-6">All Tips</h2>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
            {allTips.map((tip) => (
              <div
                key={tip.id}
                className="bg-card rounded-2xl p-6 border border-border hover:shadow-lg transition-shadow"
              >
                <div className="flex items-start space-x-4">
                  <div className="w-10 h-10 bg-primary/10 rounded-full flex items-center justify-center flex-shrink-0">
                    <Lightbulb className="h-5 w-5 text-primary" />
                  </div>
                  <div className="flex-1">
                    <h4 className="mb-2">{tip.title}</h4>
                    <p className="text-muted-foreground">{tip.tip}</p>
                  </div>
                </div>
              </div>
            ))}
          </div>
        </div>

        {/* Video Tutorials Section */}
        <div className="mt-16">
          <h2 className="mb-6">Video Tutorials</h2>
          <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
            {[1, 2, 3].map((video) => (
              <div
                key={video}
                className="bg-card rounded-2xl overflow-hidden border border-border hover:shadow-lg transition-shadow"
              >
                <div className="aspect-video bg-muted flex items-center justify-center">
                  <ChefHat className="h-12 w-12 text-muted-foreground" />
                </div>
                <div className="p-4">
                  <h4 className="mb-2">Knife Skills 101</h4>
                  <p className="text-sm text-muted-foreground">
                    Learn essential cutting techniques for faster meal prep
                  </p>
                  <p className="text-xs text-muted-foreground mt-2">12:34</p>
                </div>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}
